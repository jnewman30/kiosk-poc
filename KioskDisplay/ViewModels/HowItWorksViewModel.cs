using System.Windows;

namespace KioskDisplay.ViewModels
{
    public class HowItWorksViewModel : ScrollerViewModel
    {
        public HowItWorksViewModel() : base()
        {
            RestartInactivityTimer();
        }

        protected override ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/HowItWorks");
        }
    }
}
