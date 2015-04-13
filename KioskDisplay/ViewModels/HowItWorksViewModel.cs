using System.Windows;

namespace KioskDisplay.ViewModels
{
    public class HowItWorksViewModel : ScrollerViewModel
    {
        protected override ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/HowItWorks");
        }
    }
}
