using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class ShoppingCartTests
    {
        private Customer _customer;
        private InventoryManager _inventoryManager;
        private ShoppingCart _shoppingCart;

        private PhysicalBook _book1;
        private PhysicalBook _book2;
        private PhysicalBook _book3;

        [TestInitialize]
        public void setup()
        {
            _customer = new("alibaba@hotgirls.no");
            _inventoryManager = new();
            _shoppingCart = new(_inventoryManager, _customer.CustomerId);

            _book1 = new PhysicalBook("978-3-8747-4427-0", "Lord of the Rings: Two Towers", 299, 322, BookCoverType.Hardcover);
            _book2 = new PhysicalBook("978-0-7330-7673-2", "Witcher", 370, 456, BookCoverType.Hardcover);
            _book3 = new PhysicalBook("0-3599-3099-9", "Snømannen", 599, 588, BookCoverType.Paperback, "Ola Normann", "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));

            _inventoryManager.AddBook(_book1, 5);
            _inventoryManager.AddBook(_book2, 8);
        }

        [TestMethod]
        public void RemoveFromCart_RemoveBookFromCart_ShouldRemoveBookIfLast()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1);

            // Act
            _shoppingCart.RemoveFromCart(_book1);

            // Assert
            Assert.IsFalse(_shoppingCart.ItemsInCart.TryGetValue(_book1, out _));
        }

        [TestMethod]
        public void RemoveFromCart_DecreaseQuantityIfNotLast_ShouldKeepBookIfNotLast()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1, 2);

            // Act
            _shoppingCart.RemoveFromCart(_book1);

            // Assert
            Assert.IsTrue(_shoppingCart.ItemsInCart.TryGetValue(_book1, out _));
        }

        [TestMethod]
        public void RemoveFromCart_RemoveBookThatDoesntExist_ThrowsInvalidOperationException()
        {
            // Act and arrange
            Assert.ThrowsException<InvalidOperationException>(() => _shoppingCart.RemoveFromCart(_book3));
        }

        [TestMethod]
        public void RemoveFromCart_CalculateSubTotal_ShouldReturnCorrectSubTotal()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1, 2);
            _shoppingCart.AddToCart(_book2);
            
            var expectedSubTotal = _book1.Price * 2 + _book2.Price;

            // Act
            var result = _shoppingCart.CalculateSubTotal();

            // Assert
            Assert.AreEqual(expectedSubTotal, result);
        }

        [TestMethod]
        public void CalculateSubTotal_SumEmptyCart_ShouldReturnZero()
        {
            // Arrange
            var expectedResult = 0;

            // Act
            var result = _shoppingCart.CalculateSubTotal();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void ClearCart_ClearsItemsInCart_CartShouldBeEmpty()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1, 2);
            _shoppingCart.AddToCart(_book2);
            var expectedResult = 0;

            // Act
            _shoppingCart.ClearCart();

            // Assert
            Assert.AreEqual(expectedResult, _shoppingCart.ItemsInCart.Count);
        }
    }
}
