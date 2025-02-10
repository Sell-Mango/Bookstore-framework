using System.Text.RegularExpressions;

namespace Topologic.BookStore.Framework.Utilities
{
    public static class CustomerValidator
    {
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!email.Contains('@')) return false;
            if (!email.Contains('.')) return false;
            return true;
        }
    }
}
