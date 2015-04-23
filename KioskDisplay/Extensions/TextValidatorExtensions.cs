using System.Text.RegularExpressions;

namespace KioskDisplay.Extensions
{
	public static class TextValidatorExtensions
	{
		public static bool IsValidEmailAddress(this string s)
		{
			var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
			return regex.IsMatch(s);
		}

		public static bool IsValidPhoneNumber(this string s)
		{
			var regex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
			return regex.IsMatch(s);
		}
	}
}
