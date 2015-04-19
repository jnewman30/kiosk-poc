using System.ComponentModel;

namespace KioskDisplay.ViewModels
{
    public class ReviewsViewModel : ScrollerViewModel
    {
        public ReviewsViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            RestartInactivityTimer();
        }

        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/Reviews"); ;
        }
    }
}
