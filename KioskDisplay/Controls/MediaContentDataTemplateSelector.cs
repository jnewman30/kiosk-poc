using System.Windows;
using System.Windows.Controls;

namespace KioskDisplay.Controls
{
    public class MediaContentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate VideoTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var path = (string)item;

            if(string.IsNullOrEmpty(path) && !System.IO.File.Exists(path))
            {
                return StringTemplate;
            }

            var ext = System.IO.Path.GetExtension(path);

            if(ext == ".jpg" || ext == ".png")
            {
                return ImageTemplate;
            }
            if(ext == ".mpg" || ext == "mp4" || ext == "wmv")
            {
                return VideoTemplate;
            }

            return StringTemplate;
        }
    }
}
