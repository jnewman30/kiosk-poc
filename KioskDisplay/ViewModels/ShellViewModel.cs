using KioskDisplay.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace KioskDisplay.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public static DependencyProperty PagesProperty = 
            DependencyProperty.Register("Pages",
                typeof(ObservableCollection<string>), typeof(ShellViewModel));

        public ObservableCollection<string> Pages
        {
            get { return (ObservableCollection<string>)GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }

        public static DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage",
                typeof(string), typeof(ShellViewModel));

        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        private int _currentPageIndex;

        public ShellViewModel()
        {
            //if (!DesignerProperties.GetIsInDesignMode(this))
            //{
            //    Pages = new ObservableCollection<string>(
            //        Directory.GetFiles("./Slides", "*.xaml", SearchOption.TopDirectoryOnly));

            //    _currentPageIndex = 0;
            //    CurrentPage = Pages[_currentPageIndex];
            //}
        }

        RelayCommand _navigateToCommand;
        public ICommand NavigateToCommand
        {
            get
            {
                if(_navigateToCommand == null)
                {
                    _navigateToCommand = new RelayCommand(
                        p => CurrentPage = p.ToString(), 
                        p => !string.IsNullOrEmpty(p.ToString()));
                }
                return _navigateToCommand;
            }
        }

        RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if(_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(p => Close(), p => CanClose());
                }
                return _closeCommand;
            }
        }

        private void Close()
        {
            App.Current.MainWindow.Close();
        }

        private bool CanClose()
        {
            return true;
        }

        RelayCommand _navigateNextCommand;
        public ICommand NavigateNextCommand
        {
            get
            {
                if (_navigateNextCommand == null)
                {
                    _navigateNextCommand = new RelayCommand(p => NavigateNext(), p => CanNavigateNext());
                }
                return _navigateNextCommand;
            }
        }

        private void NavigateNext()
        {
            if(_currentPageIndex < Pages.Count -1)
            {
                _currentPageIndex++;
            }
            else
            {
                _currentPageIndex = 0;
            }
            CurrentPage = Pages[_currentPageIndex];
        }

        private bool CanNavigateNext()
        {
            return true;
        }

        RelayCommand _navigatePreviousCommand;
        public ICommand NavigatePreviousCommand
        {
            get
            {
                if (_navigatePreviousCommand == null)
                {
                    _navigatePreviousCommand = new RelayCommand(p => NavigatePrevious(), p => CanNavigatePrevious());
                }
                return _navigatePreviousCommand;
            }
        }

        private void NavigatePrevious()
        {
            if(_currentPageIndex > 0)
            {
                _currentPageIndex--;
            }
            else
            {
                _currentPageIndex = Pages.Count - 1;
            }
            CurrentPage = Pages[_currentPageIndex];
        }

        private bool CanNavigatePrevious()
        {
            return true;
        }
    }
}
