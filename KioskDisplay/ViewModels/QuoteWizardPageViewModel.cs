using System.IO;
using System.Windows.Input;
using KioskDisplay.Commands;
using KioskDisplay.Extensions;
using NLog;
using NLog.Targets;

namespace KioskDisplay.ViewModels
{
	public class QuoteWizardPageViewModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }

		public string PhoneNumber { get; set; }

		//public string Address { get; set; }

		//public string City { get; set; }

		//public string State { get; set; }

		//public string ZipCode { get; set; }

		private RelayCommand _sendEmailCommand;
		public ICommand SendEmailCommand
		{
			get { return _sendEmailCommand ?? (_sendEmailCommand = new RelayCommand(ExecuteEmail, CanExecuteEmail)); }
		}

		private static bool CanExecuteEmail(object obj)
		{
			return true;
		}

		private void ExecuteEmail(object obj)
		{
#if DEBUG
			FirstName = "Erik";
			LastName = "Gabbard";
			EmailAddress = "erikgabbard@gmail.com";
			PhoneNumber = "10234567890";
#endif

			var html = XmlHelper.Transform(this, @".\Resources\Prospect.xslt");
			var autoResponseBody = File.ReadAllText(@".\Resources\AutoResponseEmailBody.html");

			var emailLogger = LogManager.GetLogger("email");
			var fileLogger = LogManager.GetLogger("file");

			if (EmailAddress.IsValidEmailAddress() && PhoneNumber.IsValidPhoneNumber())
			{
				//send the prospect data email
				emailLogger.Log(LogLevel.Info, html);
				fileLogger.Log(LogLevel.Info, "");

				//send the autoresponse
				var mailTarget = (MailTarget)LogManager.Configuration.FindTargetByName("gmail");
				if (!string.IsNullOrEmpty(EmailAddress))
				{
					mailTarget.To = EmailAddress;
					emailLogger.Log(LogLevel.Info, autoResponseBody);
				}
			}
		}
	}
}
