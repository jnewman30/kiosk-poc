using KioskDisplay.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public static DependencyProperty StatusMessageProperty =
            DependencyProperty.Register("StatusMessage",
                typeof(string), typeof(ShellViewModel));

        public string StatusMessage
        {
            get { return (string)GetValue(StatusMessageProperty); }
            set { SetValue(StatusMessageProperty, value); }
        }

        public static DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage",
                typeof(string), typeof(ShellViewModel));

        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public ShellViewModel() : base()
        {
            CurrentPage = "./Pages/HowItWorks.xaml";

            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
        }

        RelayCommand _navigateToCommand;
        public ICommand NavigateToCommand
        {
            get
            {
                if(_navigateToCommand == null)
                {
                    _navigateToCommand = new RelayCommand(
                        ExecuteNavigateToCommand, 
                        CanExecuteNavigateToCommand);
                }
                return _navigateToCommand;
            }
        }

        protected void ExecuteNavigateToCommand(object parameter)
        {
            CurrentPage = parameter.ToString();
        }

        protected bool CanExecuteNavigateToCommand(object parameter)
        {
            if(parameter == null)
            {
                return false;
            }
            return !string.IsNullOrEmpty(parameter.ToString());
        }

        protected override void OnUserIdle()
        {
            StatusMessage = "User Idle";
            CurrentPage = "./Pages/Dormant.xaml";
        }

        protected override void OnUserActive()
        {
            StatusMessage = "User Active";
        }
    }
}
