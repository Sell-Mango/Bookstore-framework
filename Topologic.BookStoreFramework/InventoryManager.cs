using System.Collections.ObjectModel;
using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// A manager class for storing items in a book store inventory.
    /// Valid operations include adding, removing, and searching for books.
    /// Valid types are derived from the <see cref="Book"/> class.
    /// </summary>
    public class InventoryManager
    {
        private readonly Dictionary<Book, int> _booksInventory;

        /// <summary>
        /// Creates a new instance of an InventoryManager class with an empty inventory.
        /// </summary>
        public InventoryManager()
        {
            _booksInventory = [];
        }

        /// <summary>
        /// Creates a new instance of an InventoryManager class with an existing inventory.
        /// Creates a deep copy of the provided <paramref name="booksInventory">.
        /// </summary>
        /// <param name="booksInventory">An existing Dictionary of books to be added.</param>
        public InventoryManager(IDictionary<Book, int> booksInventory)
        {
            _booksInventory = new Dictionary<Book, int>(booksInventory) ?? throw new ArgumentNullException(nameof(booksInventory), "Inventory cannot be null.");
        }

        /// <summary>
        /// Gets the current inventory of the store.
        /// </summary>
        /// <value>Dictionary of all books and quantities in inventory.</value>
        public ReadOnlyDictionary<Book, int> BooksInventory => _booksInventory.AsReadOnly();

        /// <summary>
        /// Adds a Book to <see cref="BooksInventory">, or increases the number of copies if already present.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        /// <param name="numberOfCopies">Number of copies to be added of given book.</param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome.</returns>
        /// <exception cref="ArgumentNullException">Thrown if provided <paramref name="book"> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numberOfCopies"/> is zero or negative.</exception>
        public BookOperationResult AddBook(Book book, int numberOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (numberOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numberOfCopies), "number of copies must be 1 or higher.");

            if (_booksInventory.ContainsKey(book))
            {
                _booksInventory[book] += numberOfCopies;
                return BookOperationResult.Increased;
            }
            else
            {
                _booksInventory.Add(book, numberOfCopies);
            }
            return BookOperationResult.Added;
        }

        public BookOperationResult DecreaseBook(Book book, int numberOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (numberOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numberOfCopies), "Amount of books to decrease must be 1 or higher.");

            if (!_booksInventory.TryGetValue(book, out int copiesInInventory))
            {
                throw new KeyNotFoundException($"Book with title {book.Title} (ISBN: {book.Isbn}) not found in inventory. Be sure to add it to inventory first.");
            }
            if (copiesInInventory == 0)
            {
                throw new OutOfStockException($"Book with title {book.Title} (ISBN: {book.Isbn}) is out of stock. Increase quantity by adding it first.");
            }
            if (copiesInInventory < numberOfCopies)
            {
                throw new OutOfStockException($"Cannot remove {numberOfCopies} copies of book with title {book.Title} (ISBN: {book.Isbn}) from inventory because only {copiesInInventory} copies are available.");
            }

            _booksInventory[book] -= numberOfCopies;
            return BookOperationResult.Decreased;

        }

        /// <summary>
        /// Removes a Book from Inventory, or decreases if number of copies remaining in inventory is greater than 1.
        /// </summary>
        /// <param name="book">The book to be removed.</param>
        /// <param name="numberOfCopies">Number of copies to be removed of the given book.</param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome.</returns>
        /// <exception cref="ArgumentNullException">Thrown if provided <paramref name="book"> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numberOfCopies"/> is zero or negative.</exception>
        public BookOperationResult RemoveBook(Book book, int numberOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (numberOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numberOfCopies), "Copies to remove must be 1 or higher.");
            if (!_booksInventory.TryGetValue(book, out _))
            {
                throw new KeyNotFoundException($"Book with title {book.Title} and ISBN {book.Isbn} not found in inventory. Try checking again with another title or ISBN.");
            }
            _booksInventory.Remove(book);
            return BookOperationResult.Removed;
        }

        /// <summary>
        /// Searches a Book in <see cref="BooksInventory"/> by a given title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>A <see cref="Book"/> that matches the provided title. Must explicit be casted to one of its derived types, like <see cref="PhysicalBook"/></returns>
        /// <exception cref="ArgumentException">Thrown if no books matching <paramref name="title" /> is found for <see cref="Book.Title"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="title" /> is null, empty or whitepspace only.</exception>
        public Book FindBookByTitle(string title)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
            foreach (var bookEntryX in _booksInventory)
            {
                if (bookEntryX.Key.Title.Equals(title))
                {
                    return bookEntryX.Key;
                }
            }
            throw new KeyNotFoundException($"No books found by title {title}. Are you sure it exists in inventory?");
        }

        public bool TryFindBookByTite(string title, out Book? book)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
            
            foreach(var bookX in _booksInventory)
            {
                if(bookX.Key.Title.Equals(title))
                {
                    book = bookX.Key;
                    return true;
                }
            }
            book = null;
            return false;
        }

        /// <summary>
        /// Searches a book in <see cref="BooksInventory"> by ISBN.
        /// </summary>
        /// <param name="isbn">A valid ISBN.</param>
        /// <returns>A <see cref="Book"/> that matches the provided ISBN. Must explicit be casted to one of its derived types, like <see cref="PhysicalBook"/></returns>
        /// <exception cref="ArgumentException">Thrown if provided <paramref name="isbn"/> format is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if no books matching <paramref name="isbn"> is found for <see cref="Book.Isbn"/>.</exception>
        public Book FindBookByIsbn(string isbn)
        {
            if(!IsbnValidator.IsValidIsbn(isbn)) throw new IsbnFormatException("Invalid ISBN. It must be either 10 or 13 letter format.");
            foreach (var bookEntryX in BooksInventory)
            {
                if (bookEntryX.Key.Isbn.Equals(isbn))
                {
                    return bookEntryX.Key;
                }
            }
            throw new KeyNotFoundException($"No books by {isbn} found. Are you sure it exists in inventory?");
        }

        public bool TryFindBookByIsbn(string isbn, out Book book)
        {
            if (!IsbnValidator.IsValidIsbn(isbn)) throw new IsbnFormatException("Invalid ISBN. It must be either 10 or 13 letter format.");
            foreach (var bookX in _booksInventory)
            {
                if (bookX.Key.Title.Equals(isbn))
                {
                    book = bookX.Key;
                    return true;
                }
            }
            book = null;
            return false;
        }
    }
}
