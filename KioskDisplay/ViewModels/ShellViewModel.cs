using KioskDisplay.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

        public static DependencyProperty CurrentPageUrlProperty =
            DependencyProperty.Register("CurrentPageUrl",
                typeof(string), typeof(ShellViewModel));

        public string CurrentPageUrl
        {
            get { return (string)GetValue(CurrentPageUrlProperty); }
            set { SetValue(CurrentPageUrlProperty, value); }
        }

        public ShellViewModel() : base()
        {
            CurrentPageUrl = "./Pages/HowItWorks.xaml";

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
            CurrentPageUrl = parameter.ToString();
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
            CurrentPageUrl = "./Pages/Dormant.xaml";
        }

        protected override void OnUserActive()
        {
            StatusMessage = "User Active";
        }
    }
}
