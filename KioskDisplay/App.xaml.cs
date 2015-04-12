using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace KioskDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DispatcherTimer _activityTimer;
        private bool _mainWindowLoaded = false;
        private bool _isUserActive = false;

        public event EventHandler UserIdle = delegate { };
        public event EventHandler UserActive = delegate { };

        public App()
        {
            LoadCompleted += App_LoadCompleted;
        }

        void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            InitializeActivityMonitoring();
        }

        private void InitializeActivityMonitoring()
        {
            App.Current.MainWindow.Loaded += MainWindow_Loaded;

            InputManager.Current.PreProcessInput += OnActivity;

            _activityTimer = new DispatcherTimer(
                TimeSpan.FromMinutes(1),
                DispatcherPriority.ApplicationIdle,
                OnInactivity,
                Application.Current.Dispatcher);
            _activityTimer.Tick += OnInactivity;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindowLoaded = true;
        }

        void OnActivity(object sender, PreProcessInputEventArgs e)
        {
            if (!_mainWindowLoaded)
            {
                return;
            }

            // Only assume activity on mouse and keyboard events...
            var inputEventArgs = e.StagingItem.Input;
            if (!(inputEventArgs is MouseEventArgs) && !(inputEventArgs is KeyboardEventArgs))
            {
                return;
            }

            // If we have activity restart the timer...
            _activityTimer.Stop();
            _activityTimer.Start();

            if (!_isUserActive)
            {
                _isUserActive = true;
                // Fire the user active event...
                UserActive(this, new EventArgs());
            }
        }

        void OnInactivity(object sender, EventArgs e)
        {
            if (!_isUserActive)
            {
                return;
            }
            _isUserActive = false;
            // Fire the user idle event...
            UserIdle(this, new EventArgs());
        }

    }
}
