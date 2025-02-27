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
        /// Creates a new instance of an <see cref="InventoryManager"/> class with an existing inventory.
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
        /// Adds a <see cref="Book"/> to <see cref="BooksInventory">, or increases the number of copies if already present.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        /// <param name="numberOfCopies">Number of copies to be added of given <see cref="Book"/>.</param>
        /// <returns><see cref="BookOperationResult.Added"/> if a <see cref="Book"/> is successfully added, <see cref="BookOperationResult.Increased"/> if the book already exists and number of copies increases.</returns>
        /// <exception cref="ArgumentNullException">Thrown if provided <paramref name="book"> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numberOfCopies"/> is 0 or negative.</exception>
        public BookOperationResult AddBook(Book book, int numberOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (numberOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numberOfCopies), "Number of copies must be 1 or higher.");

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

        /// <summary>
        /// Decreases the number of copies of a <see cref="Book"/> in inventory. If the number of copies reaches zero, the book stays in the inventory. Use <see cref="RemoveBook"/> to remove it completely.
        /// </summary>
        /// <param name="book">A <see cref="Book"/> to find and decrease amount of copies from.</param>
        /// <param name="numberOfCopies">Number of copies to decrease.</param>
        /// <returns><see cref="BookOperationResult.Decreased"/> if the <see cref="Book"/> exists and number of copies is successfully decreased.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numberOfCopies"/> is 0 or negative number.</exception>
        /// <exception cref="KeyNotFoundException"> Thrown if <paramref name="book"/> does not exist in inventory.</exception>
        /// <exception cref="OutOfStockException">Thrown if <paramref name="numberOfCopies"/> is greater than the actual number of copies in stock for the found <paramref name="book"/>.</exception>
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
        /// Removes a <see cref="Book"/> from Inventory, only of the number of copies is 0 or <paramref name="canRemoveAll"/> is set to true.
        /// </summary>
        /// <param name="book">The book to be removed.</param>
        /// <param name="canRemoveAll">Allows removing a book even if there are copies left. Defaults to false.</param>
        /// <returns><see cref="BookOperationResult.Removed"/> if the <see cref="Book"/> is successfully removed from the inventory.</returns>
        /// <exception cref="ArgumentNullException">Thrown if provided <paramref name="book"> is null.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <paramref name="book"/> is not found in inventory.</exception>"
        /// <exception cref="InvalidOperationException">Thrown if trying to remove a book while there still are copies left and <paramref name="canRemoveAll"/> is false.</exception>
        public BookOperationResult RemoveBook(Book book, bool canRemoveAll = false)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            if (!_booksInventory.TryGetValue(book, out int copiesInInventory))
            {
                throw new KeyNotFoundException($"Book with title {book.Title} and ISBN {book.Isbn} not found in inventory. Try checking again with another title or ISBN.");
            }
            if (copiesInInventory > 0 && !canRemoveAll)
            {
                throw new InvalidOperationException($"Cannot remove all copies of the book {book.Title}. Either allow removing all by changing canRemoveAll to true, or use DecreaseBook method to set the available number to 0 first.");
            }
            _booksInventory.Remove(book);
            return BookOperationResult.Removed;
        }

        /// <summary>
        /// Gets a <see cref="Book"/> in <see cref="BooksInventory"/> by a given <see cref="Book.Title"/>.
        /// </summary>
        /// <param name="title">Name of the book to find.</param>
        /// <returns>A <see cref="Book"/> that matches the provided title. Must explicit be casted to one of its derived types, like <see cref="PhysicalBook"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no books matching <paramref name="title" /> is found for <see cref="Book.Title"/>.</exception>
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

        /// <summary>
        /// Gets a <see cref="Book"/> in <see cref="BooksInventory"/> by a given <see cref="Book.Title"/>.
        /// </summary>
        /// <param name="title">Name of the book to find.</param>
        /// <param name="book">The book that's found in <see cref="BooksInventory"/>.Is set to null if no books is found</param>
        /// <returns>True if <see cref="BooksInventory"/> contains the desired <see cref="Book"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="title"/> is null or empty.</exception>
        public bool TryFindBookByTitle(string title, out Book? book)
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
        /// Gets a book in <see cref="BooksInventory"> by a given <see cref="Book.Isbn"/>.
        /// </summary>
        /// <param name="isbn">A valid ISBN.</param>
        /// <returns>A <see cref="Book"/> that matches the provided ISBN. Must explicit be casted to one of its derived types, like <see cref="PhysicalBook"/>.</returns>
        /// <exception cref="IsbnFormatException">Thrown if provided <paramref name="isbn"/> format is invalid.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if no books matching <paramref name="isbn"> is found for <see cref="Book.Isbn"/>.</exception>
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

        /// <summary>
        /// Gets a <see cref="Book"/> in <see cref="BooksInventory"/> by a given <see cref="Book.Isbn"/>.
        /// </summary>
        /// <param name="isbn">A valid ISBN.</param>
        /// <param name="book">The book that's found in <see cref="BooksInventory"/>. Is set to null if no books is found.</param>
        /// <returns>True if <see cref="BooksInventory"/> contains the desired <see cref="Book"/>, otherwise false.</returns>
        /// <exception cref="IsbnFormatException">Thrown if provided <paramref name="isbn"/> format is invalid.</exception>
        public bool TryFindBookByIsbn(string isbn, out Book? book)
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
