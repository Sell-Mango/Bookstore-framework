using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Topologic.BookStore.Framework.Utilities;

namespace Topologic.BookStore.Framework.Models
{
    public abstract class Book : IEquatable<Book>
    {
        private string _title;
        private readonly string _isbn;
        private decimal _price = 0;
        private string _description = string.Empty;

        protected Book(string title, string isbn)
        {
            Title = title;
            if(!BookValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;
        }

        protected Book(string title, string isbn, decimal price, params string[] authorNames)
        {

            Title = title;
            Price = price;
            if (!BookValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;

            foreach (var authorName in authorNames)
            {
                AddAuthorName(authorName);
            }
        }

        protected Book(string title, string isbn, decimal price, string[] authorNames, string description, string language, string publisher, DateTime releaseDate)
        {
            Title = title;
            Price = price;
            if (!BookValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;

            Description = description;
            Language = language;
            Publisher = publisher;
            ReleaseDate = releaseDate;


            foreach (var authorName in authorNames)
            {
                AddAuthorName(authorName);
            }
        }

        public string ISBN => _isbn;
        public Collection<string> AuthorNames { get; } = [];
        public string Language { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

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

        public bool Equals(Book? otherBook)
        {
            if (otherBook == null) return false;
            return ISBN == otherBook.ISBN;
        }

        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ISBN == ((Book) obj).ISBN) return true;
            return false;

        }

        public override string ToString()
        {
            var mainInfo = $"Title: {Title}, ISBN: {ISBN}";
            if(Price != 0)
            {
                mainInfo += $", Price: {Price}";
            }
            if(AuthorNames.Count > 0)
            {
                mainInfo += $", Authors: {string.Join(",", AuthorNames)}";
            }
            if (ReleaseDate > DateTime.MinValue)
            {
                mainInfo += $", Release date: {ReleaseDate}";
            }

            return mainInfo;
        }

        public void AddAuthorName(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentException("Author name cannot be empty", nameof(authorName));
            }

            AuthorNames.Add(authorName);
        }
    }
}
