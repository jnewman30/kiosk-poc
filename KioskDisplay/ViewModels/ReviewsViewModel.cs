﻿
namespace KioskDisplay.ViewModels
{
    public class ReviewsViewModel : ScrollerViewModel
    {
        protected override System.Windows.ResourceDictionary LoadContent()
        {
            return GetResourceDictionary("Resources/Reviews.xaml");
        }
    }
}