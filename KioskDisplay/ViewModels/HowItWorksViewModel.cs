using System.Windows;

namespace KioskDisplay.ViewModels
{
    public class HowItWorksViewModel : ScrollerViewModel
    {
        protected override ResourceDictionary LoadContent()
        {
            return GetResourceDictionary("Resources/HowItWorks.xaml");
        }
    }
}
