using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.UnitTests.Models
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void AddFundsToWallet_SuccesfullyAddingFundsToWallet_CorrectFundsIsAdded()
        {
            // Arrange
            double expectedFunds = 42;
            Customer customer1 = new("alibaba@hotgirls.com");

            // Act
            customer1.AddFundsToWallet(42);

            // Arrange
            Assert.AreEqual(expectedFunds, customer1.Wallet);
        }

        [TestMethod]
        public void AddFundsToWallet_AddNegativeFunds_ThrowsArgumentException()
        {
            // Arrange
            Customer customer1 = new("bogaloo@again.no");

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => customer1.AddFundsToWallet(-42));

        }
        [TestMethod]
        public void AddToOrderHistory_AddingOrderToOrderHistory_ShouldReturnAddedorder()
        {
            // Arrange
            Customer customer1 = new("bogaloo@again.no");
            Order order = new(customer1.CustomerId, DateTime.Now, 42, []);

            // Act
            customer1.AddToOrderHistory(order);

            // Assert
            Assert.AreEqual(order, customer1.OrderHistory.FirstOrDefault());
        }
    }
}
