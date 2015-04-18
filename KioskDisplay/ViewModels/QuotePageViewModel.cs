using System.Collections.Generic;
using System.Windows;

namespace KioskDisplay.ViewModels
{
	public class QuotePageViewModel : ScrollerViewModel
	{
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
