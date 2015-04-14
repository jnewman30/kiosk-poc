using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using KioskDisplay.Extensions;

namespace KioskDisplay.ViewModels
{
    public class DormantViewModel : ScrollerViewModel
    {
        private DispatcherTimer _autoScrollTimer;

        public DormantViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            _autoScrollTimer = new DispatcherTimer(
                TimeSpan.FromSeconds(Properties.Settings.Default.AutoContentScrollIntervalSeconds),
                DispatcherPriority.Render,
                TimerElapsed,
                Application.Current.Dispatcher);
        }

        protected override System.Windows.ResourceDictionary LoadContent()
        {
            var resources = GetResourceDictionaryFromFolder("./Resources/Dormant");
            try
            {
                foreach (var resource in resources.Values)
                {
                    if (resource is MediaElement)
                    {
                        var mediaElement = (MediaElement)resource;
                        if (mediaElement.Source.IsVideo())
                        {
                            mediaElement.MediaEnded += mediaElement_MediaEnded;
                        }
                    }
                }
            }
            catch { }
            return resources;
        }

        protected override void OnUserActive()
        {
            _autoScrollTimer.IsEnabled = false;
        }

        protected override void OnUserIdle()
        {
            _autoScrollTimer.IsEnabled = true;
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

        void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (sender is MediaElement)
            {
                NextItemCommand.Execute(null);
            }
        }

        void TimerElapsed(object sender, EventArgs e)
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
    }
}
