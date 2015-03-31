
namespace KioskDisplay.ViewModels
{
    public class VideosViewModel : ScrollerViewModel
    {
        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionary("Resources/Videos.xaml");
        }
    }
}
