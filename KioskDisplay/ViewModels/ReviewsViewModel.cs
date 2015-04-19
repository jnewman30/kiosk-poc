
namespace KioskDisplay.ViewModels
{
    public class ReviewsViewModel : ScrollerViewModel
    {
        public ReviewsViewModel() : base()
        {
            RestartInactivityTimer();
        }

        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionaryFromFolder("./Resources/Reviews"); ;
        }
    }
}
