using System.Windows.Controls;

namespace KioskDisplay.Pages
{
    public abstract class PageBase : Page
    {
        public PageBase()
        {
            KeepAlive = false;
            Unloaded += PageBase_Unloaded;
        }

        void PageBase_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }
}
