using KioskDisplay.Commands;
using System.Windows.Controls;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class VideosViewModel : ScrollerViewModel
    {
        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionary("Resources/Videos.xaml");
        }

        RelayCommand _playVideoCommand;
        public ICommand PlayVideoCommand
        {
            get
            {
                if(_playVideoCommand == null)
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
            var video = objVideo as MediaElement;
            if(video == null)
            {
                return;
            }
            video.Play();
        }

        private void StopVideo(object objVideo)
        {
            var video = objVideo as MediaElement;
            if (video == null)
            {
                return;
            }
            video.Stop();
        }
    }
}
