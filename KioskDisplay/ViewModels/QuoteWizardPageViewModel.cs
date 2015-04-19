using System.Windows.Input;
using KioskDisplay.Commands;
using NLog;

namespace KioskDisplay.ViewModels
{
	public class QuoteWizardPageViewModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string ZipCode { get; set; }

		private RelayCommand _sendEmailCommand;
		public ICommand SendEmailCommand
		{
			get
			{


				if (_sendEmailCommand == null)
				{
					_sendEmailCommand = new RelayCommand(ExecuteEmail, CanExecuteEmail);
				}

				return _sendEmailCommand;
			}

		}

		private static bool CanExecuteEmail(object obj)
		{
			return true;
		}

		private static void ExecuteEmail(object obj)
		{
			var emailLogger = LogManager.GetLogger("email");
			emailLogger.Log(LogLevel.Info, "Test");

			//var message = new MailMessage();
			//message.To.Add(new MailAddress(string.Format("{0} <{1}>", "Erik Gabbard", "erikgabbard@gmail.com")));
			//message.From = new MailAddress("erikgabbard@gmail.com");

			//var smtp = new SmtpClient
			//{
			//	Host = "smtp.gmail.com",

			//};

			//smtp.Send(message);
		}
	}
}
