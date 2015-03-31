using KioskDisplay.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public static DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage",
                typeof(string), typeof(ShellViewModel));

        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public ShellViewModel()
        {
            CurrentPage = "./Pages/HowItWorks.xaml";
        }

        RelayCommand _navigateToCommand;
        public ICommand NavigateToCommand
        {
            get
            {
                if(_navigateToCommand == null)
                {
                    _navigateToCommand = new RelayCommand(
                        p => CurrentPage = p.ToString(), 
                        p => !string.IsNullOrEmpty(p.ToString()));
                }
                return _navigateToCommand;
            }
        }
    }
}
