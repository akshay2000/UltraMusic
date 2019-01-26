using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMusic.Portable.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.Media;
using Windows.Storage.Streams;
using Windows.UI.Core;

namespace UltraMusic.UWP.Helpers
{
    internal class MediaTransportHelper
    {
        private readonly SystemMediaTransportControls controls;
        private readonly MainViewModel mainViewModel;

        public MediaTransportHelper(MainViewModel mainViewModel)
        {
            controls = SystemMediaTransportControls.GetForCurrentView();
            this.mainViewModel = mainViewModel;

            controls.IsPlayEnabled = controls.IsPauseEnabled = true;
            controls.ButtonPressed += Controls_ButtonPressed;

            this.mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(mainViewModel.NowPlaying):
                    var updater = controls.DisplayUpdater;
                    updater.Type = MediaPlaybackType.Music;

                    var nowPlaying = mainViewModel.NowPlaying;
                    updater.MusicProperties.Artist = updater.MusicProperties.AlbumArtist =
                        string.IsNullOrWhiteSpace(nowPlaying.Artist)
                        ? nowPlaying.Album
                        : nowPlaying.Artist;

                    updater.MusicProperties.AlbumTitle = nowPlaying.Album;
                    updater.MusicProperties.Title = nowPlaying.Title;
                    if (!string.IsNullOrEmpty(nowPlaying.AlbumArt))
                        updater.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri(nowPlaying.AlbumArt));

                    updater.Update();

                    break;
                case nameof(mainViewModel.PlayerState):
                    var playerState = mainViewModel.PlayerState;
                    switch (playerState)
                    {
                        case PlayerState.Playing:
                            controls.PlaybackStatus = MediaPlaybackStatus.Playing;
                            break;
                        case PlayerState.Paused:
                            controls.PlaybackStatus = MediaPlaybackStatus.Paused;
                            break;
                        case PlayerState.Idle:
                        default:
                            controls.PlaybackStatus = MediaPlaybackStatus.Stopped;
                            break;
                    }
                    break;
            }
        }

        private async void Controls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                case SystemMediaTransportControlsButton.Pause:
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await mainViewModel.TogglePlayPause();
                    });
                    break;               
                case SystemMediaTransportControlsButton.Next:
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await mainViewModel.Next();
                    });
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await mainViewModel.Previous();
                    });
                    break;
            }
        }
    }
}
