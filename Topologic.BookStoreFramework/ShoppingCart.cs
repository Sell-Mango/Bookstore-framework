using System.Collections.ObjectModel;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ShoppingCart
    {
        private readonly Dictionary<Book, int> _itemsInCart;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventoryManager"></param>
        /// <param name="customerId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ShoppingCart(InventoryManager inventoryManager, string customerId)
        {
            InventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager), "Inventory cannot be null");
            CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId), "Customer cannot be null");
            _itemsInCart = [];
        }

        public ReadOnlyDictionary<Book, int> ItemsInCart => _itemsInCart.AsReadOnly();
        public InventoryManager InventoryManager { get; private set; }
        public string CustomerId { get; private set; }

        /// <summary>
        /// Adds a <see cref="Book"/> to cart times
        /// </summary>
        /// <param name="book"></param>
        /// <param name="numOfCopies"></param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome</returns>
        /// <exception cref="ArgumentException"></exception>
        public BookActionMessage AddToCart(Book book, int numOfCopies = 1)
        {
            if (numOfCopies < 1) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Copies to add cannot be zero or negative");
            
            if (!InventoryManager.Inventory.TryGetValue(book, out var copiesInInventory)) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Book does not exist");
            
            if (numOfCopies > copiesInInventory) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Quantity exceeds whats in stock");
            
            if (_itemsInCart.ContainsKey(book) && ItemsInCart[book] + numOfCopies > copiesInInventory) throw new ArgumentOutOfRangeException(nameof(numOfCopies), "Quantity exceeds whats in stock");

            if (_itemsInCart.TryAdd(book, numOfCopies))
            {
                return BookActionMessage.Added;
            }
            else
            {
                _itemsInCart[book] += numOfCopies;
                return BookActionMessage.Increased;
            }
        }

        /// <summary>
        /// Removes a Book from cart times
        /// </summary>
        /// <param name="book"></param>
        /// <param name="copiesToRemove"></param>
        /// <returns>A <see cref="BookActionMessage"/> bases on the outcome</returns>
        /// <exception cref="ArgumentException"></exception>
        public BookActionMessage RemoveFromCart(Book book, int copiesToRemove = 1)
        {
            if (copiesToRemove < 1) throw new ArgumentOutOfRangeException(nameof(copiesToRemove), "Copies to remove cannot be zero or negative");

            if(_itemsInCart.TryGetValue(book, out int copiesInCart))
            {
                if(copiesInCart > copiesToRemove)
                {
                    _itemsInCart[book] -= copiesToRemove;
                    return BookActionMessage.Decreased;
                }
                else
                {
                    _itemsInCart.Remove(book);
                    return BookActionMessage.Removed;
                }
            }
            return BookActionMessage.NotFound;
        }

        /// <summary>
        /// Calculates sum of all Books in cart
        /// </summary>
        /// <returns>Sum total cost as double</returns>
        public double CalculateSubTotal()
        {
            double total = 0;

            foreach(var bookEntryX in ItemsInCart)
            {
                total += bookEntryX.Key.Price * bookEntryX.Value;
            }

            return total;
        }

        /// <summary>
        /// Clears the cart
        /// </summary>
        public void ClearCart()
        {
            _itemsInCart.Clear();
        }
    }
}
