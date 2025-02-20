﻿namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// A manager class for handling payments in a book store.
    /// Valid operations include validating a customer, and processing a purchase order.
    /// Must provide an <see cref="InventoryManager"/> for checking against items in stock before a customer can purchase orders.
    /// </summary>
    public class PaymentManager
    {
        /// <summary>
        /// Creates a new instance of a PaymentManager class with an existing inventory manager.
        /// </summary>
        /// <param name="inventoryManager">An inventory og books.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="inventoryManager"/> is null.</exception>
        public PaymentManager(InventoryManager inventoryManager)
        {
            InventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager), "Inventory cannot be null");
        }

        /// <summary>
        /// Gets the inventory manager for the payment manager.
        /// </summary>
        /// <value>Inventory manager of books for the payment manager to check current stock.</value>
        public InventoryManager InventoryManager { get; private set; }

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
            if (currentShoppingCart.ItemsInCart.Count < 1) throw new ArgumentOutOfRangeException(nameof(currentShoppingCart), "Shopping cart must contain atleast one item to make an order.");

            if (!customer.CustomerId.Equals(currentShoppingCart.CustomerId)) throw new ArgumentException("Invalid user", nameof(customer));
            return true;
        }

        /// <summary>
        /// Completes the purchase made by a <see cref="Customer"/> in a <see cref="ShoppingCart"/>.
        /// Validates a <see cref="Customer"/> has enough funds to purchase items.
        /// Creates a new <see cref="Order"/> and adds it to the <see cref="Customer"/> order history,
        /// then remove bought items from <see cref="InventoryManager"/>.
        /// </summary>
        /// <param name="customer">The customer making an order.</param>
        /// <param name="currentShoppingCart">Shopping cart of the <paramref name="customer"/> making an order.</param>
        /// <returns>True if <paramref name="customer"/> successfully buys an order and stock items is removed from inventory. Otherwise false.</returns>
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

                customer.AddToOrdersHistory(order);

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
