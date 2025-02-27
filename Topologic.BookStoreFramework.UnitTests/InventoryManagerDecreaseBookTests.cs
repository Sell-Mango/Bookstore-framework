using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class InventoryManagerDecreaseBookTests
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
        public void DecreaseBook_DecreaseBookQuantityByOne_BookQuantityShouldBeDecreasedByOne()
        {
            // Arrange
            int expectedQuantity = 2;
            _inventoryManager.AddBook(_book1, 3);

            // Act
            _inventoryManager.DecreaseBook(_book1);

            // Assert
            Assert.AreEqual(expectedQuantity, _inventoryManager.BooksInventory[_book1]);
        }

        [TestMethod]
        public void DecreaseBook_DecreaseBookQuantityToZero_BookShouldStillBeInInventory()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);

            // Act
            _inventoryManager.DecreaseBook(_book1, 3);

            // Assert
            Assert.IsTrue(_inventoryManager.BooksInventory.ContainsKey(_book1));

        }

        [TestMethod]
        public void DecreaseBook_NotFoundInInventory_ThrowsKeyNotFoundException()
        {
            // Arrange
            _inventoryManager.AddBook(_book1);

            // Act and assert
            Assert.ThrowsException<KeyNotFoundException>(() => _inventoryManager.DecreaseBook(_book2));
        }

        // TODO: Number to decrease exceeds the quantity in inventory
        [TestMethod]
        public void DecreaseBook_DecreaseBookQuantityExceedsInventory_ThrowsOutOfStockException()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);

            // Act and assert
            Assert.ThrowsException<OutOfStockException>(() => _inventoryManager.DecreaseBook(_book1, 4));
        }



    }
}
