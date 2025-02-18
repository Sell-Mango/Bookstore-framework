using System.Text.RegularExpressions;

namespace Topologic.BookStoreFramework.Utilities
{
    public static class IsbnValidator
    {
        private const string ISBNREGEX = @"^(?:ISBN(?:-1[03])?:? )?(?=[-0-9 ]{17}$|[-0-9X ]{13}$|[0-9X]{10}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?(?:[0-9]+[- ]?){2}[0-9X]$";
        public static bool IsValidIsbn(string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return false;

            return Regex.IsMatch(isbn, ISBNREGEX, RegexOptions.IgnoreCase);
        }
    }
}