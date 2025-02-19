namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// A manager for handling payment, Must provide an <see cref="InventoryManager"/> for checking against items in stock before a customer can purchase orders.
    /// </summary>
    public class PaymentManager
    {
        /// <summary>
        /// Instansiates a PaymentManager for handling payments.
        /// </summary>
        /// <param name="inventoryManager"></param>
        /// <exception cref="ArgumentException"></exception>
        public PaymentManager(InventoryManager inventoryManager)
        {
            InventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager), "Inventory cannot be null");
        }

        public InventoryManager InventoryManager { get; private set; }

        /// <summary>
        /// Validate if correct <see cref="Customer"/> is purchasing items in a <see cref="ShoppingCart"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="currentShoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool ValdiateCorrectCustomer(Customer customer, ShoppingCart currentShoppingCart)
        {
            ArgumentNullException.ThrowIfNull(customer);
            ArgumentNullException.ThrowIfNull(currentShoppingCart);
            if (currentShoppingCart.ItemsInCart.Count < 1) throw new ArgumentOutOfRangeException(nameof(currentShoppingCart), "Shoopping cart cannot be empty or negative");

            if (!customer.CustomerId.Equals(currentShoppingCart.CustomerId)) throw new ArgumentException("Invalid user", nameof(currentShoppingCart));
            return true;
        }

        /// <summary>
        /// Calculates if a <see cref="Customer"/> has enough funds to purchase items in <see cref="ShoppingCart"/>
        /// Creates a new <see cref="Order"/> and adds it to the <see cref="Customer"/> order history
        /// Then remove items from <see cref="InventoryManager"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="currentShoppingCart"></param>
        /// <returns></returns>
        public bool PurchaseOrder(Customer customer, ShoppingCart currentShoppingCart)
        {
            double amountToPay = currentShoppingCart.CalculateSubTotal();
            if (customer.Wallet >= amountToPay)
            {
                var order = new Order(
                    customer.CustomerId,
                    DateTime.Now,
                    amountToPay,
                    new Dictionary<Book, int>(currentShoppingCart.ItemsInCart)
                );

                customer.AddToOrderHistory(order);

                foreach (var bookInCartX in currentShoppingCart.ItemsInCart)
                {
                    InventoryManager.RemoveBook(bookInCartX.Key, bookInCartX.Value);
                }

                currentShoppingCart.ClearCart();

                return true;

            }
            return false;
        }
    }
}
