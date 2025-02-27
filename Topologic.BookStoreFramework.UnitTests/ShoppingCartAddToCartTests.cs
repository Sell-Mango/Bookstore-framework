using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class ShoppingCartAddToCartTests
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
        public void AddToCart_AddingBooksIfInStockToCart_ExpectedBookIsAddedToCart()
        {
            // Arrange
            var expectedBook = _book1;

            // Act
            _shoppingCart.AddToCart(_book1);

            // Assert
            Assert.IsTrue(_shoppingCart.ItemsInCart.TryGetValue(expectedBook, out _));
        }

        [TestMethod]
        public void AddToCart_AddingExistingBookToCart_ShouldIncreaseQuantityOfBook()
        {
            // Arrange
            int expectedQuantity = 3;
            _shoppingCart.AddToCart(_book1);

            // Act
            _shoppingCart.AddToCart(_book1, 2);

            // Assert
            if (_shoppingCart.ItemsInCart.TryGetValue(_book1, out int actualQuantity))
            {
                Assert.AreEqual(expectedQuantity, actualQuantity);
            }
        }

        [TestMethod]
        public void AddToCart_AddingBookToCartOutOfStock_ThrowsOutOfStockException()
        {
            // Arrange
            _inventoryManager.AddBook(_book3); // Adding a book to inventory
            _inventoryManager.DecreaseBook(_book3); // Decrease book to 0 in stock

            // Act and Assert
            Assert.ThrowsException<OutOfStockException>(() => _shoppingCart.AddToCart(_book3));
        }

        [TestMethod]
        public void AddToCart_AddingMoreBooksThanAvailableInStock_ThrowsOutOfStockException()
        {
            // Arrange
            _shoppingCart.AddToCart(_book1, 5);

            // Act and Assert
            Assert.ThrowsException<OutOfStockException>(() => _shoppingCart.AddToCart(_book1));
        }

        [TestMethod]
        public void AddToCart_AddNegativeQuantityToCart_ThrowsArgumenOutOfRangeException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _shoppingCart.AddToCart(_book1, -1));
        }
    }
}
