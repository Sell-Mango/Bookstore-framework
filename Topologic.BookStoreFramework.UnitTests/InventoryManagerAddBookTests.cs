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
            _book1 = new PhysicalBook("978-7-3218-6224-1");
            _book2 = new PhysicalBook("978-7-7139-4939-0", "Lord of the Rings: Two Towers", 299, 322, BookCoverType.Hardcover);
            _book3 = new PhysicalBook("0-6341-6967-X", "Snømannen", 599, 588, BookCoverType.Paperback, "Ola Normann", "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));
            _book4 = new PhysicalBook("978-7-7139-4939-0"); // Duplicate of book2
            _book5 = new PhysicalBook("978-7-7139-4939-0"); // Duplicate of book2

           _audioBook1 = new AudioBook("978-0-4980-8315-0", "Heksene", 199, new TimeSpan(5, 6, 22), "Navn Navnesen");
           _audioBook2 = new AudioBook("0-2676-2756-4", "A journey to the west", 329, new TimeSpan(8, 33, 12), "Roald Dahl");
           _eBook1 = new EBook("0-4177-8938-6", "ETitle1", 299, 12);
        }

        [TestMethod]
        public void AddBook_AddOneNewBook_ShouldAddOneItemToInventory()
        {
            // Arrange
            int expectedValue = 1;

            // Act
            _inventoryManager.AddBook(_book2);

            // Assert
            Assert.AreEqual(expectedValue, _inventoryManager.BooksInventory.Count);
        }

        
        [TestMethod]
        public void AddBook_AddBookWithExistingISBN_ShouldIncreaseQuantity()
        {
            // Arrange
            int firstExpectedQuantity = 2;
            int secondExpectedQuantity = 3;

            // Act & Assert
            _inventoryManager.AddBook(_book2);
            _inventoryManager.AddBook(_book4);
            Assert.AreEqual(firstExpectedQuantity, _inventoryManager.BooksInventory[_book2]);
            
            _inventoryManager.AddBook(_book5);
            Assert.AreEqual(secondExpectedQuantity, _inventoryManager.BooksInventory[_book2]);
        }

        
        [TestMethod]
        public void AddBook_AddMultipleQuantitiesOfSameBook_ShouldReturnMessageIncreased()
        {
            // Arrange
            var expectedMessage = BookOperationResult.Increased;

            // Act
            _inventoryManager.AddBook(_book2);
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
            _inventoryManager.AddBook(_book2);
            _inventoryManager.AddBook(_book1);
            _inventoryManager.AddBook(_book3);
            _inventoryManager.AddBook(_audioBook1);
            _inventoryManager.AddBook(_audioBook2);
            _inventoryManager.AddBook(_book4);
            _inventoryManager.AddBook(_book5);
            _inventoryManager.AddBook(_eBook1);

            // Assert
            Assert.AreEqual(expectedUniquieItemCount, _inventoryManager.BooksInventory.Count);
        }

        
        [TestMethod]
        public void AddBook_AddNegativeQuantity_ThrowsArgumentOutOfRangeException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _inventoryManager.AddBook(_book2, -1));
        }
    }
}
