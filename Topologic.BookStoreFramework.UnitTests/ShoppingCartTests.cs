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

            _inventoryManager.AddBook(_book1, 2);
            _inventoryManager.AddBook(_book2, 8);

        }

        [TestMethod]
        public void AddToCart_AddingBooksIfInStockToCart_ShouldAddBookToCart()
        {
            // Arrange
            var excpectedMessage = BookOperationResult.Added;

            // Act
            var result = _shoppingCart.AddToCart(_book1);

            // Assert
            Assert.AreEqual(excpectedMessage, result);
        }

        [TestMethod]
        public void AddToCart_AddingExistingBookToCart_ShouldIncreaseQuantityOfBook()
        {
            // Arrange
            var excpectedMessage = BookOperationResult.Increased;
            _shoppingCart.AddToCart(_book1);

            // Act
            var result = _shoppingCart.AddToCart(_book1);

            // Assert
            Assert.AreEqual(excpectedMessage, result);
        }

        [TestMethod]
        public void AddToCart_AddingMoreBooksThanAvailableInStock_ArgumentOutOfRangeException()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1, 2);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _shoppingCart.AddToCart(_book1, 1));

        }

        [TestMethod]
        public void AddToCart_AddNegativeQuantityToCart_ThrowsArgumenOutOfRangeException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _shoppingCart.AddToCart(_book1, -1));
        }


        [TestMethod]
        public void RemoveFromCart_RemoveBookFromCart_ShouldRemoveBookIfLast()
        {
            // Arrange
            var expectedMessage = BookOperationResult.Removed;
            _shoppingCart.AddToCart(_book1);

            // Act
            var result = _shoppingCart.RemoveFromCart(_book1);

            // Assert
            Assert.IsTrue(expectedMessage.Equals(result));
        }

        [TestMethod]
        public void RemoveFromCart_DecreaseQuantityIfNotLast_ShouldKeepBookIfNotLast()
        {
            // Arrange
            var expectedMessage = BookOperationResult.Decreased;
            _shoppingCart.AddToCart(_book1, 2);

            // Act
            var result = _shoppingCart.RemoveFromCart(_book1);

            // Assert
            Assert.IsTrue(expectedMessage.Equals(result));

        }

        [TestMethod]
        public void RemoveFromCart_RemoveBookThatDoesntExist_ReturnMessageNotFound()
        {
            // Arrange
            var expectedMessage = BookOperationResult.NotFound;
            // Act
            var result = _shoppingCart.RemoveFromCart(_book3);

            // Assert
            Assert.IsTrue(expectedMessage.Equals(result));
        }

        [TestMethod]
        public void RemoveFromCart_AddNegativeQuantityToCart_ArgumentOutOfRangeException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _shoppingCart.RemoveFromCart(_book1, -1));
        }

        [TestMethod]
        public void CalculateSubTotal_SumPriceOfBooksInCart_ShouldReturnSumOfCart()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1, 2);
            _shoppingCart.AddToCart(_book2, 4);
            var expectedResult = 2078;

            // Act
            var result = _shoppingCart.CalculateSubTotal();

            // Assert
            Assert.AreEqual(expectedResult, result);
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
