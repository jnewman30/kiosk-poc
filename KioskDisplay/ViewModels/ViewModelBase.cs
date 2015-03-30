using System.ComponentModel;
using System.Windows;

namespace KioskDisplay.ViewModels
{
    public class ViewModelBase : DependencyObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
