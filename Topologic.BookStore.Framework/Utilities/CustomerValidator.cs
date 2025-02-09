using System.Text.RegularExpressions;

namespace Topologic.BookStore.Framework.Utilities
{
    public static class CustomerValidator
    {
        private const string EmailPattern = @"/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$/";
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase);
        }
    }
}
