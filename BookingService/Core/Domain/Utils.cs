using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain
{
    public static class Utils
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var regex = new Regex(@"[\w+.%_+-]+@[\w+.-][^_]+[.][a-zA-Z]{1,}");
            return regex.IsMatch(email);
        }
    }
}
