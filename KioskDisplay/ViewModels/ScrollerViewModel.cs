﻿using KioskDisplay.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public abstract class ScrollerViewModel : PageViewModelBase
    {
        public static DependencyProperty CurrentItemProperty =
            DependencyProperty.Register("CurrentItem",
                typeof(object), typeof(HowItWorksViewModel));

        public object CurrentItem
        {
            get { return GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }

        protected List<object> Content;
        protected int CurrentIndex;

        public ScrollerViewModel()
        {
            Content = new List<object>();

            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            var resources = LoadContent();
            if(resources == null)
            {
                return;
            }

            foreach (var key in resources.Keys)
            {
                if (!key.ToString().StartsWith("Content"))
                {
                    continue;
                }
                var resource = resources[key];
                Content.Add(resource);
            }

            if (Content.Count == 0)
            {
                return;
            }

            CurrentItem = Content[0];
            CurrentIndex = 0;
        }

        protected abstract ResourceDictionary LoadContent();

        public RelayCommand _nextItemCommand;
        public ICommand NextItemCommand
        {
            get
            {
                if(_nextItemCommand == null)
                {
                    _nextItemCommand = new RelayCommand(
                        p => NextItemExecute(), 
                        p => NextItemCanExecute());
                }
                return _nextItemCommand;
            }
        }

        public RelayCommand _previousItemCommand;
        public ICommand PreviousItemCommand
        {
            get
            {
                if (_previousItemCommand == null)
                {
                    _previousItemCommand = new RelayCommand(
                        p => PreviousItemExecute(),
                        p => PreviousItemCanExecute());
                }
                return _previousItemCommand;
            }
        }

        private bool PreviousItemCanExecute()
        {
            return Content != null && Content.Count > 0;
        }

        private void PreviousItemExecute()
        {
            if (Content == null)
            {
                return;
            }
            if(CurrentIndex > 0)
            {
                CurrentIndex--;
            }
            else
            {
                CurrentIndex = Content.Count - 1;
            }
            CurrentItem = Content[CurrentIndex];
        }

        private bool NextItemCanExecute()
        {
            return Content != null && Content.Count > 0;
        }

        private void NextItemExecute()
        {
            if(Content == null)
            {
                return;
            }
            if(CurrentIndex < Content.Count -1)
            {
                CurrentIndex++;
            }
            else
            {
                CurrentIndex = 0;
            }
            CurrentItem = Content[CurrentIndex];
        }
    }
}
