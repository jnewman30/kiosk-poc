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
        private DispatcherTimer _inavtivityTimer;
        private Point _inactiveMousePosition = new Point(0, 0);

        internal event EventHandler UserIdle = delegate { };
        internal event EventHandler UserActive = delegate { };

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

            _inavtivityTimer = new DispatcherTimer(
                TimeSpan.FromMinutes(LocalConfiguration.Settings.InactivityTimerInterval),
                DispatcherPriority.Normal,
                OnInactivity,
                Application.Current.Dispatcher);
            _inavtivityTimer.IsEnabled = false;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _inavtivityTimer.Start();
        }

        void PreProcessInput(object sender, PreProcessInputEventArgs e)
        {
            // Only assume activity on mouse and keyboard events...
            var inputEventArgs = e.StagingItem.Input;
            if (inputEventArgs is KeyboardEventArgs ||
                inputEventArgs is MouseEventArgs || 
                inputEventArgs is TouchEventArgs ||
                inputEventArgs is StylusEventArgs)
            {
                //if (inputEventArgs is MouseEventArgs)
                //{
                //    var currentMousePosition = Mouse.GetPosition(App.Current.MainWindow);
                //    if (currentMousePosition == _inactiveMousePosition)
                //    {
                //        return;
                //    }
                //}

                // If we have activity restart the timer...
                _inavtivityTimer.Stop();
                _inavtivityTimer.Start();

                // Fire the user active event...
                UserActive(this, new EventArgs());
            }
        }

        void OnInactivity(object sender, EventArgs e)
        {
            _inavtivityTimer.Stop();
            _inactiveMousePosition = Mouse.GetPosition(App.Current.MainWindow);

            // Fire the user idle event...
            UserIdle(this, new EventArgs());
        }

        public void StartInactivityTimer()
        {
            if(_inavtivityTimer == null)
            {
                return;
            }
            _inavtivityTimer.Start();
        }

        public void StopInactivityTimer()
        {
            if(_inavtivityTimer == null)
            {
                return;
            }
            _inavtivityTimer.Stop();
        }
    }
}
