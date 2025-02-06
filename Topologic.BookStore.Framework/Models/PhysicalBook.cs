using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.Framework.Models
{
    public class PhysicalBook : Book
    {
        private double _weight;
        private int _totalPages;
        private BookCoverType _bookCoverType;

        public PhysicalBook(string title, string isbn, decimal price, int weight, int totalPages)
            : base(title, isbn, price)
        {
            _weight = weight;
            _totalPages = totalPages;
        }


        public PhysicalBook(string title, string isbn, decimal price, int weight, int totalPages, BookCoverType bookCoverType, params string[] authorNames) 
            : base(title, isbn, price, authorNames)
        {
            Weight = weight;
            TotalPages = totalPages;
            BookCoverType = bookCoverType;
            
        }

        public PhysicalBook(string title, string isbn, decimal price, int weight, int totalPages, string[] authorNames, string description, string language, string publisher, DateTime releaseDate) 
            : base(title, isbn, price, authorNames, description, language, publisher, releaseDate)
        {

        }

        public double Weight
        {
            get => _weight;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Weight cannot be zero or a negative");

                }
                _weight = value;
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pages cannot be zero or negative");
                }
                _totalPages = value;
            }
        }
        public BookCoverType BookCoverType
        {
            get => _bookCoverType;
            set
            {
                if(!Enum.IsDefined(typeof(BookCoverType), value))
                {
                    throw new ArgumentException("Invalid book cover type");
                }
            }
        }
    }

}