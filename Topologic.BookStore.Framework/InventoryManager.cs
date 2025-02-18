using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// A manager for storing Books derived from the Book class.
    /// </summary>
    public class InventoryManager
    {
        //public readonly ReadOnlyDictionary<Book, int> inventory => _inventory.asreadonly()
        //private Dictionary<Book, Int> _inventory = [];
        private readonly Dictionary<Book, int> _inventory = [];

        /// <summary>
        /// Overload #1
        /// Sets up an empty Inventory for storing Books
        /// </summary>
        public InventoryManager()
        {
        }

        /// <summary>
        /// Overload #2
        /// Sets up a Inventory by taking in external Inventory
        /// </summary>
        /// <param name="inventory"></param>
        public InventoryManager(Dictionary<Book, int> inventory)
        {
            _inventory = inventory;
        }


        public Dictionary<Book, int> Inventory { get => _inventory; }

        /// <summary>
        /// Adds a Book to Inventory, numOfCopies times
        /// </summary>
        /// <param name="book"></param>
        /// <param name="numOfCopies"></param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public BookActionMessage AddBook(Book book, int numOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book, "Cannot be null");
            if (numOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Cannot add zero books to Inventory");

            if (!Inventory.TryAdd(book, numOfCopies))
            {
                Inventory[book] += numOfCopies;
                return BookActionMessage.Increased;
            }
            return BookActionMessage.Added;
        }

        /// <summary>
        /// Removes a Book from Inventory, numOfCopies times
        /// </summary>
        /// <param name="book"></param>
        /// <param name="numOfCopies"></param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public BookActionMessage RemoveBook(Book book, int numOfCopies = 1)
        {
            ArgumentNullException.ThrowIfNull(book);
            if (numOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Cannot add zero books to Inventory");

            if (Inventory.TryGetValue(book, out int currentNumOfCopies))
            {
                if (currentNumOfCopies <= numOfCopies)
                {
                    Inventory.Remove(book);
                    return BookActionMessage.Removed;
                }
                else
                {
                    Inventory[book] -= numOfCopies;
                    return BookActionMessage.Decreased;
                }
            }
            return BookActionMessage.NotFound;
        }

        /// <summary>
        /// Searches for a Book in Inventory by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>A <see cref="Book"/> that matches the provided title</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Book FindBookByTitle(string title)
        {
            ArgumentNullException.ThrowIfNull(title);
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
        /// Searches for a Book in Inventory by ISBN
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>A <see cref="Book"/> that matches the provided ISBN</returns>
        /// <exception cref="ArgumentException"></exception>
        public Book FindBookByIsbn(string isbn)
        {
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
