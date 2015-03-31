using System;
using System.ComponentModel;
using System.Reflection;
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

        protected ResourceDictionary GetResourceDictionary(string name)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyName = assembly.GetName();

                var resourceLocater = new Uri(
                    string.Format("/{0};component/{1}",
                    assemblyName.Name, name),
                    UriKind.Relative);

                return new ResourceDictionary { Source = resourceLocater };
            }
            catch { }
            return null;
        }
    }
}
