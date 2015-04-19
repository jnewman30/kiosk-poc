using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using KioskDisplay.Extensions;
using KioskDisplay.Commands;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class DormantViewModel : ScrollerViewModel
    {
        private DispatcherTimer _autoScrollTimer;
        private DispatcherTimer _videoTransitionTimer;
        private bool _active = false;

        public DormantViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            _autoScrollTimer = new DispatcherTimer(
                TimeSpan.FromSeconds(LocalConfiguration.Settings.AutoScrollInterval),
                DispatcherPriority.Render,
                AutoScrollTimerElapsed,
                Application.Current.Dispatcher);

            _videoTransitionTimer = new DispatcherTimer(
                TimeSpan.FromSeconds(LocalConfiguration.Settings.VideoTransitionDelay),
                DispatcherPriority.Normal,
                VideoTransitionTimerElapsed,
                Application.Current.Dispatcher);

            _videoTransitionTimer.IsEnabled = false;

        }

        ~DormantViewModel()
        {
            StopTimers();
        }

        private void StopTimers()
        {
            if (_autoScrollTimer != null)
            {
                _autoScrollTimer.Stop();
            }
            if (_videoTransitionTimer != null)
            {
                _videoTransitionTimer.Stop();
            }
        }

        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/Dormant");
        }

        private void SetMediaVolume()
        {
            if(CurrentItem is MediaElement)
            {
                var media = (MediaElement)CurrentItem;
                if(media.Source.IsVideo())
                {
                    if (_active)
                    {
                        media.Volume = LocalConfiguration.Settings.ActiveVolume;
                    }
                    else
                    {
                        media.Volume = LocalConfiguration.Settings.InactiveVolume;
                    }
                }
            }
        }

        protected override void OnUserActive()
        {
            _active = true;
            SetMediaVolume();
        }

        protected override void OnUserIdle()
        {
            _active = false;
            SetMediaVolume();
        }

        protected override void OnCurrentItemChanged(object oldItem, object newItem)
        {
            if (oldItem is MediaElement)
            {
                var oldMedia = (MediaElement)oldItem;
                if(oldMedia.Source.IsVideo())
                { 
                    oldMedia.Stop();
                }
            }
            if(newItem is MediaElement)
            {
                var newMedia = (MediaElement)newItem;
                if(newMedia.Source.IsVideo())
                {
                    newMedia.Play();
                }
            }
        }

        protected override void MediaOpened(object sender, RoutedEventArgs e)
        {
            SetMediaVolume();
        }

        protected override void MediaEnded(object sender, RoutedEventArgs e)
        {
            _videoTransitionTimer.Start();
        }

        void VideoTransitionTimerElapsed(object sender, EventArgs e)
        {
            _videoTransitionTimer.Stop();
            NextItemCommand.Execute(null);
        }

        void AutoScrollTimerElapsed(object sender, EventArgs e)
        {
            if (CurrentItem is MediaElement)
            {
                var mediaElement = (MediaElement)CurrentItem;
                if(mediaElement.Source.IsVideo())
                {
                    return;
                }
                NextItemCommand.Execute(null);
            }
        }

        private RelayCommand _unloadCommand;
        public ICommand UnloadCommand
        {
            get
            {
                if(_unloadCommand == null)
                {
                    _unloadCommand = new RelayCommand(UnloadCommandExecute);
                }
                return _unloadCommand;
            }
        }

        private void UnloadCommandExecute(object p)
        {
            StopTimers();
        }
    }
}
