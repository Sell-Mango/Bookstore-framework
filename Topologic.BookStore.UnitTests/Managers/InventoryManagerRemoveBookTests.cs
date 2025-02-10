using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStore.Framework.Managers;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.UnitTests.Managers
{
    [TestClass]
    public class InventoryManagerRemoveBookTests
    {
        private InventoryManager _inventoryManager;
        private PhysicalBook _book1;
        private PhysicalBook _book2;
        private PhysicalBook _book3;

        [TestInitialize]
        public void setup()
        {
            _inventoryManager = new InventoryManager();
            _book1 = new PhysicalBook("Lord of the Rings: Two Towers", "978-3-8747-4427-0", 299, 3.25, 322);
            _book2 = new PhysicalBook("Witcher", "978-0-7330-7673-2", 370, 3.5, 456, "Navn Navnesen");
            _book3 = new PhysicalBook("Snømannen", "0-3599-3099-9", 599, 4.345, 588, "Ola Normann", "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));
        }

        [TestMethod]
        public void RemoveBook_RemoveBookWithJustOneQuantity_RemovesBookFromInventory()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);
            _inventoryManager.AddBook(_book2);
            _inventoryManager.AddBook(_book3, 2);

            // Act
            _inventoryManager.RemoveBook(_book2);

            // Assert
            Assert.IsFalse(_inventoryManager.Inventory.ContainsKey(_book2));
        }

        [TestMethod]
        public void RemoveBook_DecreaseBookQuantity_BookShouldBeInInventoryAfterDecreasing()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);
            _inventoryManager.AddBook(_book2, 3);
            _inventoryManager.AddBook(_book3, 2);

            // Act
            _inventoryManager.RemoveBook(_book2, 2);

            // Assert
            Assert.IsTrue(_inventoryManager.Inventory.ContainsKey(_book2));

        }

        [TestMethod]
        public void RemoveBook_RemovingBook_ReturnsMessageRemoved()
        {
            // Arrange
            var expectedMessage = BookActionMessage.Removed;
            _inventoryManager.AddBook(_book1, 3);

            // Act
            var result = _inventoryManager.RemoveBook(_book1, 3);

            // Assert
            Assert.AreEqual(expectedMessage, result);

        }

        [TestMethod]
        public void RemoveBooK_DecreasingBook_ReturnsMessageDecreased()
        {
            // Arrange
            var expectedMessage = BookActionMessage.Decreased;
            _inventoryManager.AddBook(_book1, 3);

            // Act
            var result = _inventoryManager.RemoveBook(_book1, 2);

            // Assert
            Assert.AreEqual(expectedMessage, result);

        }

        [TestMethod]
        public void RemoveBook_RemoveNoExistingBook_ReturnsMessageNotFound()
        {
            // Arrange
            var expectedMessage = BookActionMessage.NotFound;
            _inventoryManager.AddBook(_book1, 3);
            _inventoryManager.AddBook(_book2, 3);
            _inventoryManager.AddBook(_book3, 2);

            _inventoryManager.RemoveBook(_book1, 3);

            // Act
            var result = _inventoryManager.RemoveBook(_book1);


            // Assert
            Assert.AreEqual(expectedMessage, result);
        }

        [TestMethod]
        public void RemoveBook_RemoveNegativeQuantity_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            _inventoryManager.AddBook(_book1, 3);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _inventoryManager.RemoveBook(_book1, -1));

        }

    }
}
