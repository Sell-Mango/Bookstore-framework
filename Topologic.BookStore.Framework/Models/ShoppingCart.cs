
using Topologic.BookStore.Framework.Managers;

namespace Topologic.BookStore.Framework.Models
{
    public class ShoppingCart
    {

        private readonly InventoryManager _inventoryManager;
        private readonly Dictionary<Book, int> _itemsInCart;
        private readonly string _customerId;

        public ShoppingCart(InventoryManager inventoryManager, string customerId)
        {
            _inventoryManager = inventoryManager ?? throw new ArgumentException("Inventory cannot be null", nameof(inventoryManager));
            _customerId = customerId ?? throw new ArgumentException("Customer cannot be null", nameof(customerId));
            _itemsInCart = [];
        }

        public Dictionary<Book, int> ItemsInCart { get => _itemsInCart; }
        public InventoryManager InventoryManager { get => _inventoryManager; }
        public string CustomeerId { get => _customerId; }


        public bool AddToCart(Book book, int numOfCopies = 1)
        {
            if (numOfCopies <= 0) throw new ArgumentException("Copies to add cannot be zero or negative", nameof(numOfCopies));

            if (InventoryManager.Inventory.TryGetValue(book, out int copiesInInventory) && copiesInInventory > numOfCopies)
            {
                if (ItemsInCart.ContainsKey(book))
                {
                    ItemsInCart[book] += numOfCopies;
                    return true;
                }
                else
                {
                    ItemsInCart.TryAdd(book, numOfCopies);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveFromCart(Book book, int copiesToRemove = 1)
        {
            if (copiesToRemove <= 0) throw new ArgumentException("Copies to remove cannot be zero or negative", nameof(copiesToRemove));

            if(ItemsInCart.TryGetValue(book, out int copiesInCart))
            {
                if(copiesInCart > copiesToRemove)
                {
                    ItemsInCart[book] -= copiesToRemove;
                    return true;
                }
                else
                {
                    ItemsInCart.Remove(book);
                    return true;
                }
            }
            return false;
        }

        public double CalculateSubTotal()
        {
            double total = 0;

            foreach(var bookEntryX in ItemsInCart)
            {
                total += bookEntryX.Key.Price * bookEntryX.Value;
            }

            return total;
        }

        public void ClearCart()
        {
            ItemsInCart.Clear();
        }
    }
}
