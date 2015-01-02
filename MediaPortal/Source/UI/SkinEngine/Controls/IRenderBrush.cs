﻿#region Copyright (C) 2007-2014 Team MediaPortal

/*
    Copyright (C) 2007-2014 Team MediaPortal
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

using MediaPortal.UI.SkinEngine.Rendering;

namespace MediaPortal.UI.SkinEngine.Controls
{
  /// <summary>
  /// Brushes that do own rendering to generate its content implement this interface. Before the brush can be used, the <see cref="RenderContent"/> must be called.
  /// </summary>
  public interface IRenderBrush
  {
    /// <summary>
    /// Renders the content of brush that will be used later as brush texture.
    /// </summary>
    /// <param name="renderContext">Render context</param>
    /// <returns><c>true</c> if successful</returns>
    bool RenderContent(RenderContext renderContext);
  }
}