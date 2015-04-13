using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace KioskDisplay.ViewModels
{
    public class DormantViewModel : ScrollerViewModel
    {
        private DispatcherTimer _autoScrollTimer;

        public DormantViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            _autoScrollTimer = new DispatcherTimer(
                TimeSpan.FromSeconds(Properties.Settings.Default.AutoContentScrollIntervalSeconds),
                DispatcherPriority.Render,
                TimerElapsed,
                Application.Current.Dispatcher);
        }

        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/Dormant", false);
        }

        void TimerElapsed(object sender, EventArgs e)
        {
            NextItemCommand.Execute(null);
        }
    }
}
