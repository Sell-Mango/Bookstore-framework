
using Topologic.BookStore.Framework.Utilities;

namespace Topologic.BookStore.Framework.Models
{

    public abstract class Book : IEquatable<Book>
    {
        private string _title = string.Empty;
        private readonly string _isbn;
        private double _price;
        private string _description = string.Empty;

        /// <summary>
        /// Overload #1
        /// Only required fields to handle a Book
        /// </summary>
        /// <param name="title"></param>
        /// <param name="isbn"></param>
        /// <param name="price"></param>
        /// <exception cref="ArgumentException"></exception>
        protected Book(string title, string isbn, double price)
        {
            Title = title;
            if(!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;

            Price = price;   
        }

        /// <summary>
        /// Overload 2#
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="isbn"></param>
        /// <param name="price"></param>
        /// <param name="authorName"></param>
        /// <exception cref="ArgumentException"></exception>
        protected Book(string title, string isbn, double price, string authorName)
        {

            Title = title;
            if (!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;
             
            Price = price;
            AuthorName = authorName;

        }

        protected Book(string title, string isbn, double price, string authorName, string description, string language, string publisher, DateTime releaseDate)
        {
            Title = title;
            if (!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;
            
            Price = price;
            AuthorName = authorName;
            Description = description;
            Language = language;
            Publisher = publisher;
            ReleaseDate = releaseDate;
        }

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

        public string Isbn { get => _isbn; }

        public double Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be zero or negative number");
                }
                _price = value;
            }
        }

        public string AuthorName { get; set; } = string.Empty;

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
        public string Language { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;


        public bool Equals(Book? otherBook)
        {
            return otherBook is not null && otherBook.Isbn == this.Isbn;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Book);
        }

        public override int GetHashCode()
        {
            return Isbn.GetHashCode();
        }

        public override string ToString()
        {
            var mainInfo = $"Title: {Title}, ISBN: {Isbn}";
            if(Price != 0)
            {
                mainInfo += $", Price: {Price}";
            }
            if(!string.IsNullOrEmpty(AuthorName))
            {
                mainInfo += $", Authors: {string.Join(",", AuthorName)}";
            }
            if (ReleaseDate > DateTime.MinValue)
            {
                mainInfo += $", Release date: {ReleaseDate}";
            }

            return mainInfo;
        }

    }
}
