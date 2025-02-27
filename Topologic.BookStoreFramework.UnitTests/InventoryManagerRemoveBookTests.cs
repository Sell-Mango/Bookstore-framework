namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class InventoryManagerRemoveBookTests
    {
        private InventoryManager _inventoryManager;
        private PhysicalBook _book1;
        private PhysicalBook _book2;

        [TestInitialize]
        public void Setup()
        {
            _inventoryManager = new InventoryManager();
            _book1 = new PhysicalBook("978-3-8747-4427-0", "Lord of the Rings: Two Towers", 299, 322, BookCoverType.Hardcover);
            _book2 = new PhysicalBook("978-0-7330-7673-2", "Witcher", 370, 456, BookCoverType.Spiral);

        }

        [TestMethod]
        public void RemoveBook_RemoveBookFromInventoryWithZeroCopiesInStock_BookIsRemoved()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);
            _inventoryManager.DecreaseBook(_book1 , 3);

            // Act
            _inventoryManager.RemoveBook(_book1);

            // Assert
            Assert.IsFalse(_inventoryManager.BooksInventory.ContainsKey(_book1));
        }

        [TestMethod]
        public void RemoveBook_ForceRemoveWhenMultipleCopiesOfABookIsLeft_BookIsRemoved()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);

            // Act
            _inventoryManager.RemoveBook(_book1, true);

            // Assert
            Assert.IsFalse(_inventoryManager.BooksInventory.ContainsKey(_book1));
        }

        [TestMethod]
        public void RemoveBook_TryRemovingBookWithMultipleCopiesWhenFlagIsFalse_InvalidOperationException()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);

            // Act and assert
            Assert.ThrowsException<InvalidOperationException>(() => _inventoryManager.RemoveBook(_book1));
        }

        [TestMethod]
        public void RemoveBook_BookNotExistsInInventory_KeyNotFoundException()
        {
            // Arrange
            _inventoryManager.AddBook(_book1);
            _inventoryManager.DecreaseBook(_book1);

            // Act and assert
            Assert.ThrowsException<KeyNotFoundException>(() => _inventoryManager.RemoveBook(_book2));
        }
    }
}
