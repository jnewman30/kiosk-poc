using System.ComponentModel;
using System.Windows;

namespace KioskDisplay.ViewModels
{
    public class AwardsViewModel : ScrollerViewModel
    {
        public AwardsViewModel() : base()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            RestartInactivityTimer();
        }

        protected override ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/Awards");
        }
    }
}
