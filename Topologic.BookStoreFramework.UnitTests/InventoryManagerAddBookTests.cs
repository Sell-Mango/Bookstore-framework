namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class InventoryManagerAddBookTests
    {
        private InventoryManager _inventoryManager;
        private PhysicalBook _book1;
        private PhysicalBook _book2;
        private PhysicalBook _book3;
        private PhysicalBook _book4;
        private PhysicalBook _book5;
        private AudioBook _audioBook1;
        private AudioBook _audioBook2;
        private EBook _eBook1;


        [TestInitialize]
        public void Setup()
        {
            _inventoryManager = new InventoryManager();
            _book1 = new PhysicalBook("Lord of the Rings: Two Towers", "978-3-8747-4427-0", 299, 3.25, 322);
            _book2 = new PhysicalBook("Witcher", "978-0-7330-7673-2", 370, 3.5, 456, "Navn Navnesen");
            _book3 = new PhysicalBook("Snømannen", "0-3599-3099-9", 599, 4.345, 588, "Ola Normann", "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));
            _book4 = new PhysicalBook("Lord of the Rings: Two Towers", "978-3-8747-4427-0", 299, 3.25, 322); // Duplicate of book1
            _book5 = new PhysicalBook("Lord of the Rings: Two Towers", "978-3-8747-4427-0", 299, 3.25, 322); // Duplicate of book1

            _audioBook1 = new AudioBook("Heksene", "978-0-4980-8315-0", 199, new TimeSpan(5, 6, 22), "Navn Navnesen");
            _audioBook2 = new AudioBook("A journey to the west", "978-9-0154-2640-1", 329, new TimeSpan(8, 33, 12), "Ola Normann", "Roald Dahl");
            _eBook1 = new EBook("ETitle1", "0-4177-8938-6", 299, 12);
        }

        [TestMethod]
        public void AddBook_AddOneNewBook_ShouldAddOneItemToInventory()
        {
            // Arrange
            int expectedValue = 1;

            // Act
            BookActionMessage result = _inventoryManager.AddBook(_book1);

            // Assert
            Assert.AreEqual(expectedValue, _inventoryManager.Inventory.Count);
        }

        [TestMethod]
        public void AddBook_AddOneNewBook_ShouldReturnMessageAdded()
        {
            // Arrange
            var message = BookActionMessage.Added;

            // Act
            var result = _inventoryManager.AddBook(_book1);

            // Assert
            Assert.AreEqual(message, result);
        }

        [TestMethod]
        public void AddBook_AddBookWithExistingISBN_ShouldIncreaseQuantity()
        {
            // Arrange
            int firstExpectedQuantity = 2;
            int secondExpectedQuantity = 3;

            // Act & Assert
            _inventoryManager.AddBook(_book1);
            _inventoryManager.AddBook(_book4);
            Assert.AreEqual(firstExpectedQuantity, _inventoryManager.Inventory[_book1]);
            _inventoryManager.AddBook(_book5);
            Assert.AreEqual(secondExpectedQuantity, _inventoryManager.Inventory[_book1]);
        }

        [TestMethod]
        public void AddBook_AddNewBookWithQuantity_QuantityShouldBeMoreThanOne()
        {
            // Arrange
            int expectedQuantity = 3;

            // Act
            _inventoryManager.AddBook(_book1);
            _inventoryManager.AddBook(_book2, 3);

            // Assert
            Assert.AreEqual(expectedQuantity, _inventoryManager.Inventory[_book2]);
        }

        [TestMethod]
        public void AddBook_AddMultipleQuantitiesOfSameBook_ShouldReturnMessageIncreased()
        {
            // Arrange
            var expectedMessage = BookActionMessage.Increased;

            // Act
            _inventoryManager.AddBook(_book1);
            _inventoryManager.AddBook(_book4);
            var result = _inventoryManager.AddBook(_book5);

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }

        [TestMethod]
        public void AddBook_AddMultipleBooks_ShouldAddMultipleItemsToInventory()
        {
            // Arrange
            int expectedUniquieItemCount = 6;

            // Act
            _inventoryManager.AddBook(_book1);
            _inventoryManager.AddBook(_book2);
            _inventoryManager.AddBook(_book3);
            _inventoryManager.AddBook(_audioBook1);
            _inventoryManager.AddBook(_audioBook2);
            _inventoryManager.AddBook(_book4);
            _inventoryManager.AddBook(_book5);
            _inventoryManager.AddBook(_eBook1);

            // Assert
            Assert.AreEqual(expectedUniquieItemCount, _inventoryManager.Inventory.Count);
        }

        [TestMethod]
        public void AddBook_AddNegativeQuantity_ThrowsArgumentOutOfRangeException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _inventoryManager.AddBook(_book1, -1));
        }

    }
}
