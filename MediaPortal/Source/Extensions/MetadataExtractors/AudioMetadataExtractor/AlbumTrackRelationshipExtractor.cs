﻿#region Copyright (C) 2007-2015 Team MediaPortal

/*
    Copyright (C) 2007-2015 Team MediaPortal
    http://www.team-mediaportal.com

    This file is part of MediaPortal 2

    MediaPortal 2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    MediaPortal 2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MediaPortal 2. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Extensions.OnlineLibraries.Matchers;
using MediaPortal.Common.MediaManagement.Helpers;

namespace MediaPortal.Extensions.MetadataExtractors.AudioMetadataExtractor
{
  class AlbumTrackRelationshipExtractor : IRelationshipRoleExtractor
  {
    private static readonly Guid[] ROLE_ASPECTS = { AudioAlbumAspect.ASPECT_ID };
    private static readonly Guid[] LINKED_ROLE_ASPECTS = { AudioAspect.ASPECT_ID };

    public Guid Role
    {
      get { return AudioAlbumAspect.ROLE_ALBUM; }
    }

    public Guid[] RoleAspects
    {
      get { return ROLE_ASPECTS; }
    }

    public Guid LinkedRole
    {
      get { return AudioAspect.ROLE_TRACK; }
    }

    public Guid[] LinkedRoleAspects
    {
      get { return LINKED_ROLE_ASPECTS; }
    }

    public string ExternalIdType
    {
      get
      {
        return ExternalIdentifierAspect.TYPE_TRACK;
      }
    }

    public bool TryExtractRelationships(IDictionary<Guid, IList<MediaItemAspect>> aspects, out ICollection<IDictionary<Guid, IList<MediaItemAspect>>> extractedLinkedAspects, bool forceQuickMode)
    {
      extractedLinkedAspects = null;

      AlbumInfo albumInfo = new AlbumInfo();
      if (!albumInfo.FromMetadata(aspects))
        return false;

      MusicTheAudioDbMatcher.Instance.UpdateAlbum(albumInfo, forceQuickMode);
      MusicBrainzMatcher.Instance.UpdateAlbum(albumInfo, forceQuickMode);

      if (albumInfo.Tracks.Count == 0)
        return false;

      extractedLinkedAspects = new List<IDictionary<Guid, IList<MediaItemAspect>>>();

      foreach (TrackInfo track in albumInfo.Tracks)
      {
        IDictionary<Guid, IList<MediaItemAspect>> trackAspects = new Dictionary<Guid, IList<MediaItemAspect>>();
        track.SetMetadata(trackAspects);

        if (trackAspects.ContainsKey(ExternalIdentifierAspect.ASPECT_ID))
          extractedLinkedAspects.Add(trackAspects);
      }
      return extractedLinkedAspects.Count > 0;
    }

    public bool TryMatch(IDictionary<Guid, IList<MediaItemAspect>> extractedAspects, IDictionary<Guid, IList<MediaItemAspect>> existingAspects)
    {
      if (!existingAspects.ContainsKey(AudioAspect.ASPECT_ID))
        return false;

      TrackInfo linkedTrack = new TrackInfo();
      if (!linkedTrack.FromMetadata(extractedAspects))
        return false;

      TrackInfo existingTrack = new TrackInfo();
      if (!existingTrack.FromMetadata(existingAspects))
        return false;

      return linkedTrack.Equals(existingTrack);
    }

    public bool TryGetRelationshipIndex(IDictionary<Guid, IList<MediaItemAspect>> aspects, IDictionary<Guid, IList<MediaItemAspect>> linkedAspects, out int index)
    {
      index = -1;

      SingleMediaItemAspect linkedAspect;
      if (!MediaItemAspect.TryGetAspect(linkedAspects, AudioAspect.Metadata, out linkedAspect))
        return false;

      int? trackNo = linkedAspect.GetAttributeValue<int?>(AudioAspect.ATTR_TRACK);
      if (!trackNo.HasValue)
        return false;

      index = trackNo.Value;
      return index >= 0;
    }

    internal static ILogger Logger
    {
      get { return ServiceRegistration.Get<ILogger>(); }
    }
  }
}
