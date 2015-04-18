using System.Windows;
using System.Windows.Controls;
using KioskDisplay.ViewModels;

namespace KioskDisplay.Controls
{
	public class QuotePageDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate QuoteWizardPageDataTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item is QuoteWizardPageViewModel)
				return QuoteWizardPageDataTemplate;

			return null;
		}
	}
}
