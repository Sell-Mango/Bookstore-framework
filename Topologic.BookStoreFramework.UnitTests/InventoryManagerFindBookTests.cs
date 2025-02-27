namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class InventoryManagerFindBookTests
    {
        private InventoryManager _inventoryManager;
        private PhysicalBook _book1;
        private PhysicalBook _book2;

        [TestInitialize]
        public void Setup()
        {
            _inventoryManager = new InventoryManager();
            _book1 = new PhysicalBook("978-3-8747-4427-0", "Lord of the Rings: Two Towers", 299, 322, BookCoverType.Paperback);
            _book2 = new PhysicalBook("978-0-7330-7673-2", "Witcher", 370, 456, BookCoverType.Hardcover);
        }

        [TestMethod]
        public void FindBookByTitle_SearchForAnExistingBook_ShouldReturnSpecifiedBook()
        {
            // Arrange
            var expectedBook = _book2;
            _inventoryManager.AddBook(expectedBook);

            // Act
            var result = _inventoryManager.FindBookByTitle("Witcher");

            // Assert
            Assert.IsTrue(expectedBook.Equals(result));
        }

        
        [TestMethod]
        public void FindBookByTitle_SearchForAnNonExistingBook_ThrowsKeyNotFoundException()
        {
            // Arrange
            _inventoryManager.AddBook(_book2);

            // Act and Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _inventoryManager.FindBookByTitle("Witcher 2"));
        }

        [TestMethod]
        public void TryFindBookByTitle_SearchForAnExistingBook_ShouldReturnTrueAndSpecifiedBook()
        {
            // Arrange
            var expectedBook = _book1;
            _inventoryManager.AddBook(expectedBook);

            // Act
            if(_inventoryManager.TryFindBookByTitle("Lord of the Rings: Two Towers", out var result))
            {
                // Assert
                Assert.AreEqual(expectedBook, result);
            }
        }

        [TestMethod]
        public void TryFindBookByTite_SearchForAnNonExistingBook_ShouldReturnFalse()
        {
            // Arrange
            _inventoryManager.AddBook(_book1);

            // Act and Assert
            Assert.IsFalse(_inventoryManager.TryFindBookByTitle("Witcher 2", out _));
        }
        
        [TestMethod]
        public void FindBookByIsbn_SearchForAnExistingBook_ShouldReturnSpecifiedBook()
        {
            // Arrange
            var expectedBook = _book1;
            _inventoryManager.AddBook(expectedBook);

            // Act
            var result = _inventoryManager.FindBookByIsbn("978-3-8747-4427-0");

            // Assert
            Assert.IsTrue(expectedBook.Equals(_book1));
        }

        [TestMethod]
        public void FindBookByisbn_SearchForAnNonExistingBook_ThrowsKeyNotFoundException()
        {
            // Arrange
            _inventoryManager.AddBook(_book1);

            // Act and Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _inventoryManager.FindBookByTitle("978-3-8747-4452-5"));
        }

        [TestMethod]
        public void TryFindBookByIsbn_SearchForAnExistingBook_ShouldReturnTrueAndSpecifiedBook()
        {
            // Arrange
            var expectedBook = _book2;
            _inventoryManager.AddBook(expectedBook);

            // Act
            if (_inventoryManager.TryFindBookByIsbn("978-0-7330-7673-2", out var result))
            {
                // Assert
                Assert.AreEqual(expectedBook, result);
            }
        }

        [TestMethod]
        public void TryFindBookByIsbn_SearchForAnNonExistingBook_ShouldReturnFalse()
        {
            // Arrange
            _inventoryManager.AddBook(_book2);
            // Act and Assert
            Assert.IsFalse(_inventoryManager.TryFindBookByIsbn("978-0-7330-7673-3", out _));
        }
    }
}
