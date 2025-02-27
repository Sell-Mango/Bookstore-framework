using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// A manager class for handling payments in a book store.
    /// Valid operations include validating a customer, and processing a purchase order.
    /// Must provide an <see cref="InventoryManager"/> for checking against items in stock before a customer can purchase orders.
    /// </summary>
    public class PaymentManager
    {
        private IPaymentProcessor? _paymentProcessor;

        /// <summary>
        /// Creates a new instance of a PaymentManager class with an existing inventory manager.
        /// </summary>
        /// <param name="inventoryManager">An inventory og books.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="inventoryManager"/> is null.</exception>
        public PaymentManager(InventoryManager inventoryManager)
        {
            InventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager), "Inventory cannot be null.");
        }

        /// <summary>
        /// Gets the inventory manager for the payment manager.
        /// </summary>
        /// <value>Inventory manager of books for the payment manager to check current stock.</value>
        public InventoryManager InventoryManager { get; private set; }

        /// <summary>
        /// Gets the current payment processor for the <see cref="Customer"/> to pay for orders.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if payment processor is not set.</exception>"
        public IPaymentProcessor? PaymentProcessor
        {
            get => _paymentProcessor;
            set 
            {
                _paymentProcessor = value ?? throw new ArgumentNullException(nameof(value), "Payment processor cannot be null.");
            }
        }

        /// <summary>
        /// Clears the current payment processor after use.
        /// </summary>
        public void ClearPaymentProcessor()
        {
            _paymentProcessor = null;
        }

        /// <summary>
        /// Validate if correct <see cref="Customer"/> is purchasing items in a <see cref="ShoppingCart"/>.
        /// </summary>
        /// <param name="customer">The customer that wants to make an order.</param>
        /// <param name="currentShoppingCart">Shopping cart of the <paramref name="customer"/> making an order.</param>
        /// <returns>True if <paramref name="customer"/> is owning the checking <paramref name="currentShoppingCart"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="customer"/> or <paramref name="currentShoppingCart"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the current <paramref name="customer"/> is not owning the <paramref name="currentShoppingCart"/>.</exception>
        public bool ValdiateCorrectCustomer(Customer customer, ShoppingCart currentShoppingCart)
        {
            ArgumentNullException.ThrowIfNull(customer);
            ArgumentNullException.ThrowIfNull(currentShoppingCart);
            if (currentShoppingCart.ItemsInCart.Count < 1) throw new ArgumentOutOfRangeException(nameof(currentShoppingCart), "Shopping cart must contain atleast 1 item to make an order.");

            if (!customer.CustomerId.Equals(currentShoppingCart.CustomerId)) throw new ArgumentException("Invalid customer, id does not match the shopping cart. Have you doublechecked the customer id?", nameof(customer));
            return true;
        }

        /// <summary>
        /// Completes the purchase made by a <see cref="Customer"/> in a <see cref="ShoppingCart"/>.
        /// Validates a <see cref="Customer"/> has enough funds to purchase items.
        /// Creates a new <see cref="Order"/> if <see cref="PaymentProcessor"/> returns true, and adds it to the <see cref="Customer"/> order history,
        /// then remove bought items from <see cref="InventoryManager"/>.
        /// </summary>
        /// <param name="customer">The customer making an order.</param>
        /// <param name="currentShoppingCart">Shopping cart of the <paramref name="customer"/> making an order.</param>
        /// <returns>True if <paramref name="customer"/> successfully buys an order and stock items is removed from inventory. Otherwise false.</returns>
        /// <exception cref="PaymentProcessingException">Thrown if payment processor fails to process payment.</exception>
        public bool PurchaseOrder(Customer customer, ShoppingCart currentShoppingCart)
        {
            if (PaymentProcessor is null) throw new InvalidOperationException("Payment processor is not set. Cannot process payment unless payment method is set.");
            
            ValdiateCorrectCustomer(customer, currentShoppingCart);

            double amountToPay = currentShoppingCart.CalculateSubTotal();

            try
            {
                PaymentProcessor.ProcessPayment(customer, amountToPay);

                var order = new Order(
                    customer.CustomerId,
                    DateTime.Now,
                    amountToPay,
                    new Dictionary<Book, int>(currentShoppingCart.ItemsInCart)
                );

                customer.AddToOrdersHistory(order);

                foreach (var bookInCartX in currentShoppingCart.ItemsInCart)
                {
                    InventoryManager.DecreaseBook(bookInCartX.Key, bookInCartX.Value);
                }

                currentShoppingCart.ClearCart();

            }
            catch(InvalidOperationException)
            {
                throw new PaymentProcessingException($"Something went wrong when processing your payment with: {PaymentProcessor.PaymentMethodName}.");
            }

            finally
            {
                ClearPaymentProcessor();
            }
            return true;
        }
    }
}
