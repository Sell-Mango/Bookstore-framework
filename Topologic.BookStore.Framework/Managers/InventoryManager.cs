using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.Framework.Managers
{
    public class InventoryManager
    {
        private readonly Dictionary<Book, int> _inventory = [];

        public InventoryManager()
        {
        }

        public InventoryManager(Dictionary<Book, int> inventory)
        {
            _inventory = inventory;
        }

        public Dictionary<Book, int> Inventory { get => _inventory; }


        public BookActionMessage AddBook(Book book, int numOfCopies = 1)
        {
            if (!Inventory.TryAdd(book, numOfCopies))
            {
                Inventory[book] += numOfCopies;
                return BookActionMessage.Increased;
            }
            else
            {
                Inventory.Add(book, numOfCopies);
                return BookActionMessage.Added;
            }
        }

        public BookActionMessage RemoveBook(Book book, int numOfCopies = 1)
        {
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

        public BookActionMessage UpdateBook(Book book)
        {
            if (Inventory.TryGetValue(book, out int currentNumOfCopies))
            {
                Inventory.Remove(book);
                Inventory.Add(book, currentNumOfCopies);
                return BookActionMessage.UpdateSuccess;
            }

            return BookActionMessage.NotFound;
        }


        public Book FindBookByTitle(string title)
        {
            foreach (var bookEntryX in Inventory)
            {
                if (bookEntryX.Key.Title == title)
                {
                    return bookEntryX.Key;
                }
            }
            throw new ArgumentException("No books found by title", nameof(title));
        }

        public Book FindBookByIsbn(string isbn)
        {
            foreach (var bookEntryX in Inventory)
            {
                if (bookEntryX.Key.ISBN == isbn)
                {
                    return bookEntryX.Key;
                }
            }
            throw new ArgumentException("No books by ISBN found", nameof(isbn));
        }
    }
}
