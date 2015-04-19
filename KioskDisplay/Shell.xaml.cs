using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;

namespace KioskDisplay
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }

        private void mainFrame_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            try
            {
                var navService = NavigationService.GetNavigationService(e.TargetObject);
                navService.RemoveBackEntry();

                var application = (KioskDisplay.App)App.Current;
                application.RestartInactivityTimer();
            }
            finally
            {
                e.Handled = true;
            }
        }
    }
}
