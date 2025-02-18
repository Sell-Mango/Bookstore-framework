namespace Topologic.BookStoreFramework.UnitTests
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void PhysicalBook_Constructor_ValidIsbn10Letters_ObjectIsInitialized()
        {
            // Arrange
            string excpectedIsbn10Letters = "0-2711-2752-X";

            // Act
            var result = new PhysicalBook("Lord of the Rings: Two Towers", "0-2711-2752-X", 299, 3.25, 322);

            // Assert
            Assert.AreEqual(excpectedIsbn10Letters, result.Isbn);
        }

        [TestMethod]
        public void PhysicalBook_Constructor_ValidIsbn13Letters_ObjectIsInitialized()
        {
            // Arrange
            string expectedIsbn13Letters = "978-6-7411-4578-1";

            // Act
            var result = new PhysicalBook("Witcher", "978-6-7411-4578-1", 370, 3.5, 456, "Navn Navnesen");

            // Assert
            Assert.AreEqual(expectedIsbn13Letters, result.Isbn);
        }

        [TestMethod]
        public void PhysicalBook_Constructor_InvalidIsbn13Letters_ThrowsArgumentException()
        {
            // Arrange
            string invalidIsbn13Letters = "888-8-8362-285-32";

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new PhysicalBook("Snømannen", invalidIsbn13Letters, 599, 4.345, 588, "Ola Normann"));
        }

    }
}
