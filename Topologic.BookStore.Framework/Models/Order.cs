using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Models
{
    public class Order
    {
        private readonly string _orderId;
        private readonly string _customerId;
        private readonly DateTime _orderDateTime;
        private readonly double _orderTotal;
        private readonly Collection<Book> _orderedItems = [];
        
        public Order(string orderId, string customerId, DateTime orderDateTime, double orderTotal)
        {
            _orderId = orderId;
            _customerId = customerId;
            _orderDateTime = orderDateTime;
            _orderTotal = orderTotal;
        }

        public string OrderId { get => _orderId; }
        public string CustomerId { get => _customerId; }
        public DateTime orderDateTime { get => _orderDateTime; }
        public double OrderTotal { get => _orderTotal; }
        public Collection<Book> OrderedItems { get => _orderedItems; }
    }
}
