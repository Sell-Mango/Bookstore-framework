using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a physical book derived from <see cref="Book"/>.
    /// Can be added to the inventory of a <see cref="InventoryManager"/> and can be bought by a <see cref="Customer"/>."/>
    /// </summary>
    public class PhysicalBook : Book
    {
        private int _totalPages;
        private BookCoverType _bookCoverType;

        /// <summary>
        /// Creates a new instance of a <see cref="PhysicalBook"/> class with valid <see cref="Book.Isbn"> only (minimal constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the physical book, cannot be changed later.</param>
        /// <remarks>Exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public PhysicalBook(string isbn)
            : base(isbn)
        {
            _totalPages = 0;
        }

        /// <summary>
        /// Creates a new instance of an <see cref="PhysicalBook"/> class with basic information.
        /// </summary>
        /// <param name="isbn">A valid ISBN for the physical book, cannot be changed later.</param>
        /// <param name="title">Title of the physical book.</param>
        /// <param name="price">Price of the physical book.</param>
        /// <param name="totalPages">Number of total pages of the physical book.</param>
        /// <param name="bookCoverType">Cover type of the physical book.</param>
        /// <remarks>Some exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public PhysicalBook(string isbn, string title, double price, int totalPages, BookCoverType bookCoverType)
            : base(isbn, title, price)
        {
            TotalPages = totalPages;
            BookCoverType = bookCoverType;
        }

        /// <summary>
        /// Creates a new instance of an <see cref="PhysicalBook"/> class with all information (advanded constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the physical book, cannot be changed later.</param>
        /// <param name="title">Title of the physical book.</param>
        /// <param name="price">Price of the physical book.</param>
        /// <param name="totalPages">Number of total pages of the physical book.</param>
        /// <param name="bookCoverType">Cover type of the physical book.</param>
        /// <param name="authorName">Author name of the physical book.</param>
        /// <param name="description">A brief description of the physical book (max <see cref="MAX_DESCRIPTION_LENGTH"/> characters.</param>
        /// <param name="language">Written language for the physical book.</param>
        /// <param name="publisherName">Publisher of the physical book.</param>
        /// <param name="releaseDate">Release date for the physical book.</param>
        /// <remarks>Some exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public PhysicalBook(string isbn, string title, double price, int totalPages, BookCoverType bookCoverType, string authorName, string description, string language, string publisherName, DateTime releaseDate) 
            : base(isbn, title, price, authorName, description, language, publisherName, releaseDate)
        {
            TotalPages = totalPages;
            BookCoverType = bookCoverType;
        }

        /// <summary>
        /// Gets or sets the total number of pages of the physical book.
        /// </summary>
        /// <value>Total number of pages of the physical book.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if total pages is zero or negative.</exception>
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

        /// <summary>
        /// Gets or sets the cover type of the physical book.
        /// See <see cref="BookCoverType"/> for available options.
        /// </summary>
        /// <value>Cover type of the physical book.</value>
        /// <exception cref="ArgumentNullException">Thrown if cover type is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if cover type is <see cref="BookCoverType.None"/>.</exception>"
        public BookCoverType BookCoverType
        {
            get => _bookCoverType;
            set
            {
                if(value == BookCoverType.None)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "BookCoverType cannot be None");
                }
                _bookCoverType = value;
            }
        }
    }

}