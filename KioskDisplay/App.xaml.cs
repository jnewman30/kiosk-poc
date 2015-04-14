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
        private Point _inactiveMousePosition = new Point(0, 0);

        internal event EventHandler UserIdle = delegate { };
        internal event EventHandler UserActive = delegate { };

        internal KioskDisplay.Properties.Settings Settings
        {
            get { return KioskDisplay.Properties.Settings.Default; }
        }

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

            InputManager.Current.PreProcessInput += PreProcessInput;

            _activityTimer = new DispatcherTimer(
                //TimeSpan.FromMinutes(Settings.InactivityTimerIntervalMinutes),
                TimeSpan.FromSeconds(30d),
                DispatcherPriority.ApplicationIdle,
                OnInactivity,
                Application.Current.Dispatcher);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindowLoaded = true;
        }

        void PreProcessInput(object sender, PreProcessInputEventArgs e)
        {
            if (!_mainWindowLoaded || _isUserActive)
            {
                return;
            }

            // Only assume activity on mouse and keyboard events...
            var inputEventArgs = e.StagingItem.Input;
            if (inputEventArgs is MouseEventArgs || inputEventArgs is KeyboardEventArgs)
            {
                //if(inputEventArgs is QueryCursorEventArgs)
                //{
                //    return;
                //}
                if(inputEventArgs is MouseEventArgs)
                {
                    var currentMousePosition = Mouse.GetPosition(App.Current.MainWindow);
                    if (currentMousePosition == _inactiveMousePosition)
                    {
                        return;
                    }
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
        }

        void OnInactivity(object sender, EventArgs e)
        {
            if (!_isUserActive)
            {
                return;
            }
            _inactiveMousePosition = Mouse.GetPosition(App.Current.MainWindow);
            _isUserActive = false;
            // Fire the user idle event...
            UserIdle(this, new EventArgs());
        }
    }
}
