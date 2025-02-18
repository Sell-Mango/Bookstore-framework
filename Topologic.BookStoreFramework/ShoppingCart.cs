namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ShoppingCart
    {
        private readonly InventoryManager _inventoryManager;
        private readonly Dictionary<Book, int> _itemsInCart;
        private readonly string _customerId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventoryManager"></param>
        /// <param name="customerId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ShoppingCart(InventoryManager inventoryManager, string customerId)
        {
            _inventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager), "Inventory cannot be null");
            _customerId = customerId ?? throw new ArgumentNullException(nameof(customerId), "Customer cannot be null");
            _itemsInCart = [];
        }

        public Dictionary<Book, int> ItemsInCart { get => _itemsInCart; }
        public InventoryManager InventoryManager { get => _inventoryManager; }
        public string CustomerId { get => _customerId; }

        /// <summary>
        /// Adds a <see cref="Book"/> to cart times
        /// </summary>
        /// <param name="book"></param>
        /// <param name="numOfCopies"></param>
        /// <returns>A <see cref="BookActionMessage"/> based on the outcome</returns>
        /// <exception cref="ArgumentException"></exception>
        public BookActionMessage AddToCart(Book book, int numOfCopies = 1)
        {
            if (numOfCopies <= 0) 
                throw new ArgumentException("Copies to add cannot be zero or negative", nameof(numOfCopies));
            if (!InventoryManager.Inventory.TryGetValue(book, out var copiesInInventory)) 
                throw new ArgumentException("Book does not exist", nameof(numOfCopies));
            if (numOfCopies > copiesInInventory)
                throw new ArgumentException("Quantity exceeds whats in stock", nameof(numOfCopies));
            if (ItemsInCart.ContainsKey(book) && ItemsInCart[book] + numOfCopies > copiesInInventory)
                throw new ArgumentException("Quantity exceeds whats in stock", nameof(numOfCopies));

            if (ItemsInCart.TryAdd(book, numOfCopies))
            {
                return BookActionMessage.Added;
            }
            else
            {
                ItemsInCart[book] += numOfCopies;
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
            if (copiesToRemove <= 0) throw new ArgumentException("Copies to remove cannot be zero or negative", nameof(copiesToRemove));

            if(ItemsInCart.TryGetValue(book, out int copiesInCart))
            {
                if(copiesInCart > copiesToRemove)
                {
                    ItemsInCart[book] -= copiesToRemove;
                    return BookActionMessage.Decreased;
                }
                else
                {
                    ItemsInCart.Remove(book);
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
            ItemsInCart.Clear();
        }
    }
}
