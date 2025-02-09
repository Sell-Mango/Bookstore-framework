using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.Framework.Managers
{
    public class PaymentManager
    {
        private readonly ShoppingCart _currentShoppingCart;
        private readonly InventoryManager _inventoryManager;
        private readonly Customer _customer;

        public PaymentManager(ShoppingCart currentShoppingCart, InventoryManager inventoryManager, Customer customer)
        {
            if(ValidateShoppingCart(currentShoppingCart)) _currentShoppingCart = currentShoppingCart;

            _inventoryManager = inventoryManager ?? throw new ArgumentException("Inventory cannot be null", nameof(inventoryManager));
            _customer = customer ?? throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        public ShoppingCart CurrentShoppingCart { get => _currentShoppingCart; }
        public InventoryManager InventoryManager { get => _inventoryManager; }
        public Customer Customer { get => _customer; }

        private bool ValidateShoppingCart(ShoppingCart currentShoppingCart)
        {
            if (currentShoppingCart == null) throw new ArgumentNullException(nameof(currentShoppingCart), "ShoppingCart cannot be null");
            if (currentShoppingCart.ItemsInCart.Count == 0) throw new ArgumentException("ShooppingCart is empty", nameof(currentShoppingCart));
            if (!Customer.CustomerId.Equals(CurrentShoppingCart.CustomeerId)) throw new ArgumentException("Incorrect user", nameof(currentShoppingCart));
            return true;
        }

        private Order CreateOrder()
        {
            return new Order(
                Customer.CustomerId,
                DateTime.Now,
                CurrentShoppingCart.CalculateSubTotal(),
                new Dictionary<Book, int>(CurrentShoppingCart.ItemsInCart)
                );
        }

        public bool PurchaseOrder(double funds)
        {
            double amountToPay = CurrentShoppingCart.CalculateSubTotal();
            if(amountToPay >= funds)
            {
                foreach(var bookInCartX in CurrentShoppingCart.ItemsInCart)
                {
                    InventoryManager.RemoveBook(bookInCartX.Key, bookInCartX.Value);
                }
                Customer.AddOrder(CreateOrder());

                return true;

            }
            return false;
        }
    }
}
