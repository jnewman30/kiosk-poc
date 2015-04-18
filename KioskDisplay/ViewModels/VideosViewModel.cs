using KioskDisplay.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using KioskDisplay.Extensions;

namespace KioskDisplay.ViewModels
{
    public class VideosViewModel : ScrollerViewModel
    {
        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/Videos");
        }

        RelayCommand _playVideoCommand;
        public ICommand PlayVideoCommand
        {
            get
            {
                if (_playVideoCommand == null)
                {
                    _playVideoCommand = new RelayCommand(
                        PlayVideoExecute, PlayVideoCanExecute);
                }
                return _playVideoCommand;
            }
        }

        private bool PlayVideoCanExecute(object parameter)
        {
            return parameter != null;
        }

        private void PlayVideoExecute(object parameter)
        {
            PlayVideo(parameter);
        }

        RelayCommand _stopVideoCommand;
        public ICommand StopVideoCommand
        {
            get
            {
                if (_stopVideoCommand == null)
                {
                    _stopVideoCommand = new RelayCommand(
                        StopVideoExecute, StopVideoCanExecute);
                }
                return _stopVideoCommand;
            }
        }

        private bool StopVideoCanExecute(object parameter)
        {
            return parameter != null;
        }

        private void StopVideoExecute(object parameter)
        {
            StopVideo(parameter);
        }

        protected override void OnCurrentItemChanged(object oldItem, object newItem)
        {
            StopVideo(oldItem);
            PlayVideo(newItem);
        }

        private void PlayVideo(object objVideo)
        {
            if (objVideo is MediaElement)
            {
                var media = objVideo as MediaElement;
                if (media.Source.IsVideo())
                {
                    media.Play();
                }
            }
        }

        private void StopVideo(object objVideo)
        {
            if (objVideo is MediaElement)
            {
                var media = objVideo as MediaElement;
                if (media.Source.IsVideo())
                {
                    media.Stop();
                }
            }
        }

        protected override void MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            base.MediaOpened(sender, e);
        }

        protected override void MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            base.MediaEnded(sender, e);

            StopInactivityTimer();
            StartInactivityTimer();

            NextItemCommand.Execute(null);
        }
    }
}
