using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.Framework.Models
{
    public class PhysicalBook : Book
    {
        public PhysicalBook(string title, string isbn, decimal price, params string[] authorNames) 
            : base(title, isbn, price, authorNames)
        {

        }

        public PhysicalBook(string title, string isbn, decimal price, string[] authorNames, string description, string language, string publisher, DateTime releaseDate) 
            : base(title, isbn, price, authorNames, description, language, publisher, releaseDate)
        {

        }
    }

}