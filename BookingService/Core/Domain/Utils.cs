using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain
{
    public static partial class Utils
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var regex = Email();
            return regex.IsMatch(email);
        }

        [GeneratedRegex(@"[\w+.%_+-]+@[\w+.-][^_]+[.][a-zA-Z]{1,}")]
        private static partial Regex Email();
    }
}
