
namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents an a digital book, derived from <see cref="Book"/> class.
    /// Can be added to the inventory of a <see cref="InventoryManager"/> and can be bought by a <see cref="Customer"/>."/>
    /// </summary>
    public class EBook : Book
    {
        private double _fileSize;

        /// <summary>
        /// Creates a new instance of a <see cref="EBook"/> class with valid ISBN only (minimal constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the EBook, cannot be changed later.</param>
        /// <remarks>Exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public EBook(string isbn)
            : base(isbn)
        {
            FileSize = 0;
        }

        /// <summary>
        /// Creates a new instance of an <see cref="EBook"/> class with basic information.
        /// </summary>
        /// <param name="isbn">A valid ISBN for the e-book, cannot be changed later.</param>
        /// <param name="title">Title of the e-book.</param>
        /// <param name="price">Price of the e-book.</param>
        /// <param name="fileSize">File size of the e-book.</param>
        /// <remarks>Some exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public EBook(string isbn, string title, double price, double fileSize)
            : base(isbn, title, price)
        {
            FileSize = fileSize;
        }

        /// <summary>
        /// Creates a new instance of an <see cref="EBook"/> class with all information (advanded constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the e-book, cannot be changed later.</param>
        /// <param name="title">Title of the e-book.</param>
        /// <param name="price">Price of the e-book.</param>
        /// <param name="fileSize">File size of the e-book.</param>
        /// <param name="authorName">Author name for the e-book.</param>
        /// <param name="description">A brief description of the e-book (max <see cref="MAX_DESCRIPTION_LENGTH"/> characters.</param>
        /// <param name="language">Written language of the e-book.</param>
        /// <param name="publisher">Publisher of the e-bookk.</param>
        /// <param name="releaseDate">Release date for the e-book.</param>
        /// <remarks>Some exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public EBook(string isbn, string title, double price, double fileSize, string authorName, string description, string language, string publisher, DateTime releaseDate)
            : base(isbn, title, price, authorName, description, language, publisher, releaseDate)
        {
            FileSize = fileSize;
        }

        /// <summary>
        /// Gets or sets the file size of the e-book.
        /// </summary>
        /// <value>File size of the e-book, in megabytes.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when file size is zero or negative.</exception>"
        public double FileSize
        {
            get => _fileSize;
            set
            {

                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "File size cant be zero or negative");

                _fileSize = value;
            }
        }
    }
}
