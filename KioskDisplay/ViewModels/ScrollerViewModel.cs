using System.ComponentModel;

namespace KioskDisplay.ViewModels
{
    public class ScrollerViewModel : PageViewModelBase
    {
        public ScrollerViewModel()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
        }
    }
}
