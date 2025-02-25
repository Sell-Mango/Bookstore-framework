using System.Text.RegularExpressions;
using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework.Utilities
{
    /// <summary>
    /// Static class providing a method to validate ISBN for derived <see cref="Book"/> objects.
    /// ISBN is validated using a builtIn regular expression class <see cref="Regex"/>.
    /// </summary>
    public static class IsbnValidator
    {
        private const string ISBNREGEX = @"^(?:ISBN(?:-1[03])?:? )?(?=[-0-9 ]{17}$|[-0-9X ]{13}$|[0-9X]{10}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?(?:[0-9]+[- ]?){2}[0-9X]$";

        /// <summary>
        /// Validates a 10 or 13 digit ISBN number.
        /// </summary>
        /// <param name="isbn">provided ISBN from a derived <see cref="Book"/> object to validate.</param>
        /// <returns>True if a valid 10 or 13 digit ISBN is provided, otherwise false.</returns>
        public static bool IsValidIsbn(string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return false;

            return Regex.IsMatch(isbn, ISBNREGEX, RegexOptions.IgnoreCase);
        }
    }
}