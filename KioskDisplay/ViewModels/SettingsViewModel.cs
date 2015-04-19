using KioskDisplay.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;
using System.Reflection;

namespace KioskDisplay.ViewModels
{
    public class SettingsViewModel : PageViewModelBase
    {
        public static DependencyProperty SettingsProperty = DependencyProperty.Register(
            "Settings", typeof(LocalConfiguration), typeof(SettingsViewModel));

        public LocalConfiguration Settings
        {
            get { return (LocalConfiguration)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static DependencyProperty SettingsCodeProperty = DependencyProperty.Register(
            "SettingsCode", typeof(string), typeof(SettingsViewModel));

        public string SettingsCode
        {
            get { return (string)GetValue(SettingsCodeProperty); }
            set { SetValue(SettingsCodeProperty, value); }
        }

        public static DependencyProperty LoginVisibleProperty = DependencyProperty.Register(
            "LoginVisible", typeof(Visibility), typeof(SettingsViewModel));

        public Visibility LoginVisible
        {
            get { return (Visibility)GetValue(LoginVisibleProperty); }
            set { SetValue(LoginVisibleProperty, value); }
        }

        public static DependencyProperty SettingsVisibleProperty = DependencyProperty.Register(
            "SettingsVisible", typeof(Visibility), typeof(SettingsViewModel));

        public Visibility SettingsVisible
        {
            get { return (Visibility)GetValue(SettingsVisibleProperty); }
            set { SetValue(SettingsVisibleProperty, value); }
        }

        public SettingsViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            RestartInactivityTimer();

            Settings = LocalConfiguration.Settings;
            LoginVisible = Visibility.Visible;
            SettingsVisible = Visibility.Collapsed;
        }

        RelayCommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if(_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
                }
                return _loginCommand;
            }
        }

        private bool CanExecuteLogin(object obj)
        {
            return true;
        }

        private void ExecuteLogin(object obj)
        {
            if(SettingsCode == Settings.SettingsCode)
            {
                LoginVisible = Visibility.Collapsed;
                SettingsVisible = Visibility.Visible;
            }
        }

        RelayCommand _saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get
            {
                if (_saveSettingsCommand == null)
                {
                    _saveSettingsCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
                }
                return _saveSettingsCommand;
            }
        }

        private bool CanExecuteSave(object parameter)
        {
            return true;
        }

        private void ExecuteSave(object parameter)
        {
            Settings.Write();
        }

        RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(ExecuteClose, CanExecuteClose);
                }
                return _closeCommand;
            }
        }

        private void ExecuteClose(object parameter)
        {
            App.Current.Shutdown();
        }

        private bool CanExecuteClose(object parameter)
        {
            return true;
        }
    }
}
