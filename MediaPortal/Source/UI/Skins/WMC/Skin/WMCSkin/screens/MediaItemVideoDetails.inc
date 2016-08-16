<?xml version="1.0" encoding="utf-8"?>
<Grid
  xmlns="www.team-mediaportal.com/2008/mpf/directx"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  xmlns:fanart="clr-namespace:MediaPortal.Extensions.UserServices.FanArtService.Client;assembly=FanArtService.Client"
  xmlns:media="clr-namespace:MediaPortal.UiComponents.Media.Models.AspectWrappers;assembly=Media">

  <Grid.RowDefinitions>
    <RowDefinition Height="Auto"/>
    <RowDefinition Height="*"/>
  </Grid.RowDefinitions>
  <Grid.ColumnDefinitions>
    <ColumnDefinition Width="480"/>
    <ColumnDefinition Width="*"/>
  </Grid.ColumnDefinitions>

  <media:MediaAspectWrapper Grid.Row="0" Margin="0,-15,0,0" MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}">
    <media:MediaAspectWrapper.Template>
      <ControlTemplate>
        <StackPanel Orientation="Horizontal">
          <Label x:Name="MediaItemName" Color="{ThemeResource TextColor}" FontSize="{ThemeResource SmallFontSize}" FontFamily="SegoeUIBold"
                 DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
            <Label.Content>
              <Binding Path="Title"/>
            </Label.Content>
          </Label>
        </StackPanel>
      </ControlTemplate>
    </media:MediaAspectWrapper.Template>
  </media:MediaAspectWrapper>

  <StackPanel Orientation="Vertical" Grid.Column="0" Grid.ColumnSpan="2" Grid.Row="1" Margin="0,0,0,5">
    <media:MediaAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}">
      <media:MediaAspectWrapper.Template>
        <ControlTemplate>
          <StackPanel Orientation="Horizontal">
            <Label x:Name="Year" HorizontalAlignment="Left" FontSize="{ThemeResource SmallFontSize}"
                   DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}" Opacity="0.5"
                   Content="{Binding Path=RecordingTime, Converter={StaticResource DateFormatConverter}, ConverterParameter=g}" Color="{ThemeResource TextColor}"/>
          </StackPanel>
        </ControlTemplate>
      </media:MediaAspectWrapper.Template>
    </media:MediaAspectWrapper>
    
    <media:VideoStreamAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}">
      <media:VideoStreamAspectWrapper.Template>
        <ControlTemplate>
          <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
            <StackPanel Orientation="Horizontal">
              <Label x:Name="Duration" FontSize="{ThemeResource SmallFontSize}" Opacity="0.5"
                     Color="{ThemeResource TextColor}">
                <Label.Content>
                  <MultiBinding Converter="{StaticResource ExpressionMultiValueConverter}" ConverterParameter="{}{0}/60">
                    <Binding Path="Duration"/>
                  </MultiBinding>
                </Label.Content>
              </Label>
              <Label Content=" min." FontSize="{ThemeResource SmallFontSize}" Opacity="0.5" Color="{ThemeResource TextColor}"
                     IsVisible="{Binding Path=Duration,Converter={StaticResource ReferenceToBoolConverter}}"/>
            </StackPanel>
          </Grid>
        </ControlTemplate>
      </media:VideoStreamAspectWrapper.Template>
    </media:VideoStreamAspectWrapper>
  </StackPanel>

  <media:VideoAspectWrapper Grid.Row="1" Grid.Column="1" MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}">
    <media:VideoAspectWrapper.Template>
      <ControlTemplate>
        <StackPanel Orientation="Horizontal">
          <Label x:Name="MediaItemName" Color="{ThemeResource TextColor}" FontSize="{ThemeResource SmallFontSize}"
                 Opacity="0.5" TextTrimming="WordEllipsis"
                 DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
            <Label.Content>
              <Binding Path="StoryPlot"/>
            </Label.Content>
          </Label>
        </StackPanel>
      </ControlTemplate>
    </media:VideoAspectWrapper.Template>
  </media:VideoAspectWrapper>

</Grid>