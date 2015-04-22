using KioskDisplay.ViewModels;
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

        private void PageBase_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = (ViewModelBase)DataContext;
            viewModel.UnloadCommand.Execute(null);
        }
    }
}
