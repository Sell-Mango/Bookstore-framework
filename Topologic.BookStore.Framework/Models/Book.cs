using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Models
{
    public abstract class Book
    {
        private string _title;
        private string _ISBN;
        private decimal _price;
        private string _authorName;
        private DateTime _releaseDate;

        public string Title
        {
            get => _title;

            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(value), "Title cannot be empty");
                }

                var maxTitleLength = 200;

                if(value.Length > maxTitleLength)
                {
                    throw new ArgumentException(nameof(value), $"Title is too long, max ${maxTitleLength} characters");
                }

                _title = value;
            }
        }

        public string ISBN
        {
            get => _ISBN;

            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(value), "ISBN cannot be empty");
                }

                if(!Regex.IsMatch(value, @"^(?:ISBN(?:-1[03])?:? )?(?=[-0-9 ]{17}$|[-0-9X ]{13}$|[0-9X]{10}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?(?:[0-9]+[- ]?){2}[0-9X]$", RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException(nameof(value), "Mismatch of ISBN format");
                }

                _ISBN = value;

            }
        }

        public decimal Price
        {
            get => _price;

            set
            {
                if(value < 0)
                {
                    throw new ArgumentException(nameof(value), "Price cannot be a negative number");
                }

                _price = value;
            }
        }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
