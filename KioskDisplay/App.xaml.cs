﻿using System;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using KioskDisplay.Extensions;
using Microsoft.Win32;

namespace KioskDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DispatcherTimer _inavtivityTimer;

        internal event EventHandler UserIdle = delegate { };
        internal event EventHandler UserActive = delegate { };

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadCompleted += App_LoadCompleted;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //TODO: Log with NLog.
            var ex = (Exception)e.ExceptionObject;
            MessageBox.Show(ex.ToString());
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //TODO: Log with NLog.
            MessageBox.Show(e.Exception.ToString());
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
                if (inputEventArgs is QueryCursorEventArgs)
                {
                    return;
                }

                // If we have activity restart the timer...
                RestartInactivityTimer();

                // Fire the user active event...
                UserActive(this, new EventArgs());
            }
        }

        void OnInactivity(object sender, EventArgs e)
        {
            _inavtivityTimer.Stop();

            // Fire the user idle event...
            UserIdle(this, new EventArgs());
        }

        public void RestartInactivityTimer()
        {
            if (_inavtivityTimer == null)
            {
                return;
            }
            StopInactivityTimer();
            StartInactivityTimer();
        }

        public void StartInactivityTimer()
        {
            if (_inavtivityTimer == null)
            {
                return;
            }
            _inavtivityTimer.Start();
        }

        public void StopInactivityTimer()
        {
            if (_inavtivityTimer == null)
            {
                return;
            }
            _inavtivityTimer.Stop();
        }

	    private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
	    {
		    var textBox = sender as TextBox;

		    Debug.Assert(textBox != null, "textBox != null");
		    switch (textBox.Name)
		    {
				case "tbxPhoneNumber":
				    if (!textBox.Text.IsValidPhoneNumber())
				    {
					    textBox.Style = (Style) FindResource("TextBoxErrorStyle");
				    }
				    break;
				case "tbxEmailAddress":
					if (!textBox.Text.IsValidEmailAddress())
					{
						textBox.Style = (Style)FindResource("TextBoxErrorStyle");
					}
				    break;
		    }
	    }
    }
}
