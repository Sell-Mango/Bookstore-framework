using System.Collections.ObjectModel;
using Topologic.BookStoreFramework.Utilities;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// A manager class for storing items in a book store inventory.
    /// Valid operations include adding, removing, and searching for books.
    /// Valid types are derived from the <see cref="Book"/> class.
    /// </summary>
    public class InventoryManager
    {
        private readonly Dictionary<Book, int> _inventory;

        /// <summary>
        /// Creates a new instance of an InventoryManager class with an empty inventory.
        /// </summary>
        public InventoryManager()
        {
            _inventory = [];
        }

        /// <summary>
        /// Creates a new instance of an InventoryManager class with an existing inventory.
        /// Creates a deep copy of the provided <paramref name="inventory">.
        /// </summary>
        /// <param name="inventory">An existing Dictionary of books to be added.</param>
        public InventoryManager(Dictionary<Book, int> inventory)
        {
            _inventory = new Dictionary<Book, int>(inventory) ?? throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null");
        }

        /// <summary>
        /// Gets the current inventory of the store.
        /// </summary>
        /// <value>Dictionary of all books and quantities in inventory.</value>
        public ReadOnlyDictionary<Book, int> Inventory => _inventory.AsReadOnly();

        /// <summary>
        /// Adds a Book to <see cref="Inventory">, or increases the number of copies if already present.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        /// <param name="numOfCopies">Number of copies to be added of given book.</param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome.</returns>
        /// <exception cref="ArgumentNullException">Thrown if provided <paramref name="book"> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numOfCopies"/> is zero or negative.</exception>
        public BookActionMessage AddBook(Book book, int numOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (numOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Cannot add zero or negative amount books to Inventory");

            if (!_inventory.TryAdd(book, numOfCopies))
            {
                _inventory[book] += numOfCopies;
                return BookActionMessage.Increased;
            }
            return BookActionMessage.Added;
        }

        /// <summary>
        /// Removes a Book from Inventory, or decreases if number of copies remaining in inventory is greater than 1.
        /// </summary>
        /// <param name="book">The book to be removed.</param>
        /// <param name="numOfCopies">Number of copies to be removed of the given book.</param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome.</returns>
        /// <exception cref="ArgumentNullException">Thrown if provided <paramref name="book"> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numOfCopies"/> is zero or negative.</exception>
        public BookActionMessage RemoveBook(Book book, int numOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (numOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Cannot remove zero or negative amount books to Inventory");

            if (_inventory.TryGetValue(book, out int currentNumOfCopies))
            {
                if (currentNumOfCopies <= numOfCopies)
                {
                    _inventory.Remove(book);
                    return BookActionMessage.Removed;
                }
                else
                {
                    _inventory[book] -= numOfCopies;
                    return BookActionMessage.Decreased;
                }
            }
            return BookActionMessage.NotFound;
        }

        /// <summary>
        /// Searches a Book in <see cref="Inventory"/> by a given title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>A <see cref="Book"/> that matches the provided title.</returns>
        /// <exception cref="ArgumentException">Thrown if no books matching <paramref name="title" /> is found for <see cref="Book.Title"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="title" /> is null, empty or whitepspace only.</exception>
        public Book FindBookByTitle(string title)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null or empty");
            foreach (var bookEntryX in Inventory)
            {
                if (bookEntryX.Key.Title.Equals(title))
                {
                    return bookEntryX.Key;
                }
            }

            throw new ArgumentException("No books found by title", nameof(title));
        }

        /// <summary>
        /// Searches a book in <see cref="Inventory"> by ISBN.
        /// </summary>
        /// <param name="isbn">A valid ISBN.</param>
        /// <returns>A <see cref="Book"/> that matches the provided ISBN.</returns>
        /// <exception cref="ArgumentException">Thrown if provided <paramref name="isbn"/> format is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if no books matching <paramref name="isbn"> is found for <see cref="Book.Isbn"/>.</exception>
        public Book FindBookByIsbn(string isbn)
        {
            if(!IsbnValidator.IsValidIsbn(isbn)) throw new ArgumentException("Invalid ISBN format", nameof(isbn));
            foreach (var bookEntryX in Inventory)
            {
                if (bookEntryX.Key.Isbn.Equals(isbn))
                {
                    return bookEntryX.Key;
                }
            }
            throw new ArgumentException("No books by ISBN found", nameof(isbn));
        }
    }
}
