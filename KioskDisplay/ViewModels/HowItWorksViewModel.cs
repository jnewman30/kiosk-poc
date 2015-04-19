using System.ComponentModel;
using System.Windows;

namespace KioskDisplay.ViewModels
{
    public class HowItWorksViewModel : ScrollerViewModel
    {
        public HowItWorksViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            RestartInactivityTimer();
        }

        protected override ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/HowItWorks");
        }
    }
}
