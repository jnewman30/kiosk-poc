using System.Windows.Controls;

namespace KioskDisplay.Pages
{
    public abstract class PageBase : Page
    {
        public PageBase()
        {
            KeepAlive = false;
        }
    }
}
