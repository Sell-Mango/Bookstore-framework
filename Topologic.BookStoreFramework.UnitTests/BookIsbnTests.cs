using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class BookIsbnTests
    {
        [TestMethod]
        public void PhysicalBook_Constructor_ValidIsbn10Letters_ObjectIsInitialized()
        {
            // Arrange
            string excpectedIsbn10Letters = "0-2711-2752-X";

            // Act
            var result = new PhysicalBook("0-2711-2752-X");

            // Assert
            Assert.AreEqual(excpectedIsbn10Letters, result.Isbn);
        }

        [TestMethod]
        public void PhysicalBook_Constructor_ValidIsbn13Letters_ObjectIsInitialized()
        {
            // Arrange
            string expectedIsbn13Letters = "978-6-7411-4578-1";

            // Act
            var result = new PhysicalBook("978-6-7411-4578-1");

            // Assert
            Assert.AreEqual(expectedIsbn13Letters, result.Isbn);
        }

        [TestMethod]
        public void PhysicalBook_Constructor_InvalidIsbn13Letters_ThrowsArgumentException()
        {
            // Arrange
            string invalidIsbn13Letters = "888-8-8362-285-32";

            // Act and Assert
            Assert.ThrowsException<IsbnFormatException>(() => new PhysicalBook(invalidIsbn13Letters));
        }

    }
}
