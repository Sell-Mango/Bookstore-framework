using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Utilities
{
    public static class BookValidator
    {
        private const string IsbnRegex = @"^(?:ISBN(?:-1[03])?:? )?(?=[-0-9 ]{17}$|[-0-9X ]{13}$|[0-9X]{10}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?(?:[0-9]+[- ]?){2}[0-9X]$";
        public static bool IsValidIsbn(string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return false;

            return Regex.IsMatch(isbn, IsbnRegex, RegexOptions.IgnoreCase);
        }
    }
}