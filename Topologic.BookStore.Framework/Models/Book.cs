using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Models
{
    public abstract class Book
    {
        private string _title;
        private readonly string _ISBN;
        private decimal _price;
        private string _description;
        private DateTime _releaseDate;

        protected Book(string title, string isbn, decimal price, params string[] authorNames)
        {

            Title = title;
            Price = price;

            ValidateISBN(isbn);

            foreach (var authorName in authorNames)
            {
                AddAuthorName(authorName);
            }
        }

        protected Book(string title, string isbn, decimal price, string[] authorNames, string description, string language, string publisher, DateTime releaseDate)
        {
            Title = title;
            Price = price;
            Description = description;
            ReleaseDate = releaseDate;
            Language = language;
            Publisher = publisher;

            ValidateISBN(isbn);

            foreach (var authorName in authorNames)
            {
                AddAuthorName(authorName);
            }
        }

        public string ISBN => _ISBN;
        public Collection<string> AuthorNames { get; } = [];
        public string Language { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                const int maxTitleLength = 200;
                if (value.Length > maxTitleLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Title is too long, max {maxTitleLength} characters");
                }
                _title = value;
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be a negative number");
                }

                _price = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                var maxDescriptionLength = 5000;
                if (value.Length > maxDescriptionLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Description is too long, max {maxDescriptionLength} characters");
                }

                _description = value;
            }
        }


        public void ValidateISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot by empty", nameof(isbn));
            }

            if (!Regex.IsMatch(isbn, @"^(?:ISBN(?:-1[03])?:? )?(?=[-0-9 ]{17}$|[-0-9X ]{13}$|[0-9X]{10}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?(?:[0-9]+[- ]?){2}[0-9X]$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Mismatch of ISBN format", nameof(isbn));
            }
        }
        
        public void AddAuthorName(string authorName)
        {
            if(string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentException("Author name cannot be empty", nameof(authorName));
            }

            AuthorNames.Add(authorName);
        }
    }
}
