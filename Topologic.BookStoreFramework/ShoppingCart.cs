using System.Collections.ObjectModel;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a shopping cart for a customer.
    /// Valid operations include adding, removing, and calculating the total cost of items in the cart.
    /// Must provide an <see cref="InventoryManager"/> for checking against items in stock before a customer can add items to the cart.
    /// </summary>
    public class ShoppingCart
    {
        private readonly Dictionary<Book, int> _itemsInCart;

        /// <summary>
        /// Creates a new instance of a <see cref="ShoppingCart"/> class with an existing inventory manager and customer id."/>
        /// </summary>
        /// <param name="inventoryManager"></param>
        /// <param name="customerId"></param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="inventoryManager"/> or <paramref name="customerId"/> is empty.</exception>
        public ShoppingCart(InventoryManager inventoryManager, string customerId)
        {
            InventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager), "Inventory cannot be null");
            CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId), "Customer cannot be null");
            _itemsInCart = [];
        }

        /// <summary>
        /// Gets the items in the cart.
        /// </summary>
        /// <value>Dictionary of all books with quantities added to the cart.</value>
        public ReadOnlyDictionary<Book, int> ItemsInCart => _itemsInCart.AsReadOnly();

        /// <summary>
        /// Gets the inventory manager for the shopping cart.
        /// </summary>
        /// <value>All items currently in stock.</value>
        public InventoryManager InventoryManager { get; }

        /// <summary>
        /// Gets the customer id for the shopping cart.
        /// </summary>
        /// <value>Customer id referring to the <see cref="Customer"/> owning the shopping cart.</value>
        public string CustomerId { get; }

        /// <summary>
        /// Adds a <see cref="Book"/> or increase to <see cref="ItemsInCart"/> if stock is available in <see cref="InventoryManager"/>.
        /// </summary>
        /// <param name="book">Book existing in <see cref="InventoryManager"> to be added.</param>
        /// <param name="numOfCopies">Number of copies of a <paramref name="book"/> to be added.</param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in of these cases: 
        /// If <paramref name="numOfCopies"/> is zero or negative.
        /// If <paramref name="book"/> does not exist in <see cref="InventoryManager"/>
        /// If quantity of <paramref name="numOfCopies"/> exceeds stock available in <see cref="InventoryManager"/>.</exception>
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
        /// Removes a <see cref="Book"/> or decrease from <see cref="ItemsInCart">.
        /// </summary>
        /// <param name="book">Book existing in <see cref="ItemsInCart"> to be removed or decreased.</param>
        /// <param name="copiesToRemove">Number of copies of a <paramref name="book"/> to be removed.</param>
        /// <returns>A <see cref="BookActionMessage"/> bases on the outcome.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if copies to remove is zero or negative.</exception>
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
        /// Calculates subtotal of all items in <see cref="ItemsInCart"/>.
        /// </summary>
        /// <returns>Sum total cost <see cref="ItemsInCart"/> without handling or delivery fee.</returns>
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
        /// Clears <see cref="ItemsInCart"/>.
        /// Should be called after a successful purchase.
        /// </summary>
        public void ClearCart()
        {
            _itemsInCart.Clear();
        }
    }
}
