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
    public class PaymentManagerTests
    {
        private Customer _customer;
        private InventoryManager _inventoryManager;
        private ShoppingCart _shoppingCart;
        private PaymentManager _paymentManager;

        private PhysicalBook _book1;
        private PhysicalBook _book2;
        private PhysicalBook _book3;

        [TestInitialize]
        public void setup()
        {
            _customer = new("alibaba@hotgirls.no");
            _inventoryManager = new();
            _shoppingCart = new(_inventoryManager, _customer.CustomerId);
            _paymentManager = new(_inventoryManager);

            _book1 = new PhysicalBook("Lord of the Rings: Two Towers", "978-3-8747-4427-0", 299, 3.25, 322);
            _book2 = new PhysicalBook("Witcher", "978-0-7330-7673-2", 370, 3.5, 456, "Navn Navnesen");
            _book3 = new PhysicalBook("Snømannen", "0-3599-3099-9", 599, 4.345, 588, "Ola Normann", "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));

            _inventoryManager.AddBook(_book1, 2);
            _inventoryManager.AddBook(_book2, 8);

            _shoppingCart.AddToCart(_book1, 2);
            _shoppingCart.AddToCart(_book2, 3);
        }

        [TestMethod]
        public void PurchaseOrder_SuccessfullyPurchasesOrder_ShouldReturnTrue()
        {
            // Arrange
            _customer.AddFundsToWallet(9999);

            // Act
            var result = _paymentManager.PurchaseOrder(_customer, _shoppingCart);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PurchaseOrder_NotEnoughFundsToPurchaseOrder_ShouldReturnFalse()
        {
            // Arrange
            _customer.AddFundsToWallet(100);

            // Act
            var result = _paymentManager.PurchaseOrder(_customer, _shoppingCart);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
