namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class PaymentManagerTests
    {
        private Customer _customer;
        private InventoryManager _inventoryManager;
        private ShoppingCart _shoppingCart;
        private PaymentManager _paymentManager;

        private PhysicalBook _book1;
        private PhysicalBook _book2;
        private PhysicalBook _book3;

        [TestInitialize]
        public void setup()
        {
            _customer = new("alibaba@hotgirls.no");
            _inventoryManager = new();
            _shoppingCart = new(_inventoryManager, _customer.CustomerId);
            _paymentManager = new(_inventoryManager);

            _book1 = new PhysicalBook("978-3-8747-4427-0", "Lord of the Rings: Two Towers", 299, 322, BookCoverType.Hardcover);
            _book2 = new PhysicalBook("978-0-7330-7673-2", "Witcher", 370, 456, BookCoverType.Paperback);
            _book3 = new PhysicalBook("0-3599-3099-9", "Snømannen",  599, 588, BookCoverType.Spiral, "Ola Normann", "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));

            _inventoryManager.AddBook(_book1, 2);
            _inventoryManager.AddBook(_book2, 8);

            _shoppingCart.AddToCart(_book1, 2);
            _shoppingCart.AddToCart(_book2, 3);
        }

        [TestMethod]
        public void PurchaseOrder_SuccessfullyPurchasesOrder_ShouldReturnTrue()
        {
            // Arrange
            _customer.AddFundsToWallet(9999);

            // Act
            var result = _paymentManager.PurchaseOrder(_customer, _shoppingCart);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PurchaseOrder_NotEnoughFundsToPurchaseOrder_ShouldReturnFalse()
        {
            // Arrange
            _customer.AddFundsToWallet(100);

            // Act
            var result = _paymentManager.PurchaseOrder(_customer, _shoppingCart);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
