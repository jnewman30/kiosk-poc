using KioskDisplay.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class SettingsViewModel : PageViewModelBase
    {
        public static DependencyProperty SettingsCodeProperty = DependencyProperty.Register(
            "SettingsCode", typeof(string), typeof(SettingsViewModel),
            new PropertyMetadata(KioskDisplay.Properties.Settings.Default.SettingsCode));

        public string SettingsCode
        {
            get { return (string)GetValue(SettingsCodeProperty); }
            set { SetValue(SettingsCodeProperty, value); }
        }

        public static DependencyProperty InactivityIntervalProperty = DependencyProperty.Register(
            "InactivityInterval", typeof(double), typeof(SettingsViewModel),
            new PropertyMetadata(KioskDisplay.Properties.Settings.Default.InactivityTimerIntervalMinutes));

        public double InactivityInterval
        {
            get { return (double)GetValue(InactivityIntervalProperty); }
            set { SetValue(InactivityIntervalProperty, value); }
        }

        public static DependencyProperty AutoScrollIntervalProperty = DependencyProperty.Register(
            "AutoScrollInterval", typeof(double), typeof(SettingsViewModel),
            new PropertyMetadata(KioskDisplay.Properties.Settings.Default.AutoContentScrollIntervalSeconds));

        public double AutoScrollInterval
        {
            get { return (double)GetValue(AutoScrollIntervalProperty); }
            set { SetValue(AutoScrollIntervalProperty, value); }
        }

        public static DependencyProperty ActiveVolumeProperty = DependencyProperty.Register(
            "ActiveVolume", typeof(double), typeof(SettingsViewModel),
            new PropertyMetadata(KioskDisplay.Properties.Settings.Default.ActiveVolume));

        public double ActiveVolume
        {
            get { return (double)GetValue(ActiveVolumeProperty); }
            set { SetValue(ActiveVolumeProperty, value); }
        }

        public static DependencyProperty InactiveVolumeProperty = DependencyProperty.Register(
            "InactiveVolume", typeof(double), typeof(SettingsViewModel),
            new PropertyMetadata(KioskDisplay.Properties.Settings.Default.InactiveVolume));

        public double InactiveVolume
        {
            get { return (double)GetValue(InactiveVolumeProperty); }
            set { SetValue(InactiveVolumeProperty, value); }
        }

        public static DependencyProperty ReplaceWindowsShellProperty = DependencyProperty.Register(
            "ReplaceWindowsShell", typeof(bool), typeof(SettingsViewModel),
            new PropertyMetadata(KioskDisplay.Properties.Settings.Default.ReplaceWindowsShell));

        public bool ReplaceWindowsShell
        {
            get { return (bool)GetValue(ReplaceWindowsShellProperty); }
            set { SetValue(ReplaceWindowsShellProperty, value); }
        }

        public SettingsViewModel()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            Properties.Settings.Default.Upgrade();
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
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
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
            App.Current.MainWindow.Close();
            //TODO: Start Windows Shell
        }

        private bool CanExecuteClose(object parameter)
        {
            return true;
        }
    }
}
