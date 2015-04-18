using KioskDisplay.Commands;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class SettingsViewModel : PageViewModelBase
    {
        RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(p => Close(), p => CanClose());
                }
                return _closeCommand;
            }
        }

        private void Close()
        {
            App.Current.MainWindow.Close();
        }

        private bool CanClose()
        {
            return true;
        }
    }
}
