using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace KioskDisplay.ViewModels
{
	public class QuotePageViewModel : ScrollerViewModel
	{
        public QuotePageViewModel() : base()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            RestartInactivityTimer();
        }

		protected override ResourceDictionary LoadContent()
		{
			Content = new List<object>
			{
				new QuoteWizardPageViewModel()
			};
			return null;
		}
	}
}
