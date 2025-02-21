using Topologic.BookStoreFramework.Utilities;

namespace Topologic.BookStoreFramework
{

    /// <summary>
    /// Represents a base class for all books in the framework. 
    /// Cannot instantiate this class directly, use one of the derived like <see cref="PhysicalBook"/> and <see cref="AudioBook"/>.
    /// </summary>
    public abstract class Book : IEquatable<Book>
    {
        private const int MAX_TITLE_LENGTH = 200;
        private const int MAX_DESCRIPTION_LENGTH = 5000;

        private string _title;
        private readonly string _isbn;
        private double _price;
        private string _description;

        /// <summary>
        /// Creates a new instance of a <see cref="Book"/> class with valid ISBN only (minimal constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the Book, cannot be changed later.</param>
        /// <exception cref="ArgumentException">Thrown when ISBN is invalid.</exception>
        protected Book(string isbn)
        {
            if (!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;
            Title = string.Empty;
            _price = 0;
            Description = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="Book"/> class with basic information.
        /// </summary>
        /// <param name="isbn">A valid ISBN for the Book, cannot be changed later.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="price">Price for the book.</param>
        /// <exception cref="ArgumentException">Thrown when ISBN is invalid.</exception>
        protected Book(string isbn, string title, double price)
        {
            if (!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;
            Title = title;
            Price = price;
            Description = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="Book"/> class with all information (advanded constructor).
        /// </summary>
        /// <param name="title">Title of the book.</param>
        /// <param name="isbn">A valid ISBN for the Book, cannot be changed later.</param>
        /// <param name="price">Price for the book.</param>
        /// <param name="authorName">Author name of the book.</param>
        /// <param name="description">A brief description of the book (max <see cref="MAX_DESCRIPTION_LENGTH"/> characters.</param>
        /// <param name="language">Written language for the book.</param>
        /// <param name="publisherName">Publisher for the book.</param>
        /// <param name="releaseDate">Release date for the book.</param>
        /// <exception cref="ArgumentException"></exception>
        protected Book(string isbn, string title, double price, string authorName, string description, string language, string publisherName, DateTime releaseDate)
        {
            if (!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            _isbn = isbn;
            Title = title;
            Price = price;
            AuthorName = authorName;
            Description = description;
            Language = language;
            PublisherName = publisherName;
            ReleaseDate = releaseDate;
        }

        /// <summary>
        /// Gets the ISBN of the book.
        /// </summary>
        /// <value>ISBN of the book.</value>
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

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        /// <value>Title og the book.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length exceeds <see cref="MAX_TITLE_LENGTH"/> characters.</exception>
        public string Title
        {
            get => _title;
            set
            {
                if (value.Length > MAX_TITLE_LENGTH)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Title is too long, max {MAX_TITLE_LENGTH} characters");
                }
                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets the author name of the book.
        /// </summary>
        /// <value>The book author's name.</value>
        public string AuthorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a brief description of the book.
        /// </summary>
        /// <value>The book description.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when description exceeds <see cref="MAX_DESCRIPTION_LENGTH"/> characters.</exception>
        public string Description
        {
            get => _description;
            set
            {
                if (value.Length > MAX_DESCRIPTION_LENGTH)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Description is too long, max {MAX_DESCRIPTION_LENGTH} characters");
                }
                _description = value;
            }
        }

        /// <summary>
        /// Gets or sets the language of the book.
        /// </summary>
        /// <value>Language name of the book.</value>
        public string Language { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the publisher of the book.
        /// </summary>
        /// <value>Name of the book publisher.</value>
        public string PublisherName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the release date of the book.
        /// </summary>
        /// <value>Release date of the book.</value>
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Checks if the current book is equal to the comparing book based on <see cref="Isbn"/>.
        /// </summary>
        /// <param name="other">The other book object to compare with the current book.</param>
        /// <returns>True if the current and comparing book have the same <see cref="Isbn"/>, or false.</returns>
        public bool Equals(Book? other)
        {
            return other is not null && other.Isbn == this.Isbn;
        }

        /// <summary>
        /// Checks if the current book is equal to the comparing book.
        /// </summary>
        /// <param name="obj">The book object to compare with the current book.</param>
        /// <returns>True if the current and comparing book have the same <see cref="Isbn"/>, or false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Book);
        }

        /// <summary>
        /// Gets the hash code of the book based on <see cref="Isbn">
        /// </summary>
        /// <returns>A hashcode for the book</returns>
        public override int GetHashCode()
        {
            return Isbn.GetHashCode();
        }

        /// <summary>
        /// Generates a string representation of main information of the book.
        /// </summary>
        /// <returns>A string containg main information for the book such as <see cref="Isbn"/>, <see cref="Title"/>, <see cref="Price"/> if not zero,
        /// <see cref="AuthorName"/> if not empty and <see cref="ReleaseDate"/> if set.</returns>
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
