using System.Collections.ObjectModel;

namespace Topologic.BookStoreFramework
{
    public class Order
    {
        private readonly string _orderId;
        private readonly string _customerId;
        private readonly DateTime _orderDateTime;
        private readonly double _orderTotal;
        private readonly Dictionary<Book, int> _orderedItems;
        
        public Order(string customerId, DateTime orderDateTime, double orderTotal, Dictionary<Book, int> orderedItems)
        {
            _orderId = Guid.NewGuid().ToString();
            _customerId = customerId;
            _orderDateTime = orderDateTime;
            _orderTotal = orderTotal;
            _orderedItems = orderedItems;
        }

        public string OrderId { get => _orderId; }
        public string CustomerId { get => _customerId; }
        public DateTime OrderDateTime { get => _orderDateTime; }
        public double OrderTotal { get => _orderTotal; }
        public ReadOnlyDictionary<Book, int> OrderedItems => _orderedItems.AsReadOnly();
    }
}
