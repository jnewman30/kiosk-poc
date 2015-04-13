﻿using System;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace KioskDisplay.ViewModels
{
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ViewModelBase()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            var application = (KioskDisplay.App)Application.Current;
            application.UserActive += application_UserActive;
            application.UserIdle += application_UserIdle;
        }

        void application_UserIdle(object sender, EventArgs e)
        {
            OnUserIdle();
        }

        protected virtual void OnUserIdle() { }

        void application_UserActive(object sender, EventArgs e)
        {
            OnUserActive();
        }

        protected virtual void OnUserActive() { }

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

        protected ResourceDictionary GetResourceDictionaryFromFolder(string folder, bool isVideo = false)
        {
            var resources = new ResourceDictionary();

            try
            {
                var viewModelName = this.GetType().Name;

                var files = Directory
                    .GetFiles(Path.GetFullPath(folder))
                    .OrderBy(filename => filename);

                var count = 1;
                foreach (var file in files)
                {
                    var key = string.Format("Content-{0}-{1}", viewModelName, count);
                    var content = new MediaElement()
                    {
                        Source = new Uri(file, UriKind.Absolute)
                    };

                    if (isVideo)
                    {
                        content.LoadedBehavior = MediaState.Manual;
                    }

                    resources.Add(key, content);
                    count++;
                }
            }
            catch { }

            return resources;
        }
    }
}
