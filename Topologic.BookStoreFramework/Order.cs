﻿using System.Collections.ObjectModel;
using System.Text;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents an order made by a customer.
    /// Should not be instantiated directly, use <see cref="PaymentManager"/> to create new orders.
    /// </summary>
    public class Order
    {
        private readonly string _orderId;
        private readonly string _customerId;
        private readonly DateTime _orderDateTime;
        private readonly double _orderTotal;
        private readonly Dictionary<Book, int> _orderedItems;

        /// <summary>
        /// Creates a new instance of an Order class with all necessary information.
        /// Generates a new unique order id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderDateTime"></param>
        /// <param name="orderTotal"></param>
        /// <param name="orderedItems"></param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="customerId"/> or <paramref name="orderedItems"/>' parameters is null.</exception>
        public Order(string customerId, DateTime orderDateTime, double orderTotal, Dictionary<Book, int> orderedItems)
        {
            _orderId = Guid.NewGuid().ToString();
            _customerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
            _orderDateTime = orderDateTime;
            _orderTotal = orderTotal;
            _orderedItems = orderedItems ?? throw new ArgumentNullException(nameof(orderedItems));
        }

        /// <summary>
        /// Gets the unique id for the order.
        /// </summary>
        /// <value>Order id that is automatically generated.</value>
        public string OrderId { get => _orderId; }

        /// <summary>
        /// Gets the customer id for the order.
        /// </summary>
        /// <value>Customer id that made the order.</value>
        public string CustomerId { get => _customerId; }

        /// <summary>
        /// Gets the date and time when the order was made.
        /// </summary>
        /// <value>Order date and time.</value>
        public DateTime OrderDateTime { get => _orderDateTime; }

        /// <summary>
        /// Gets the total amount of the order.
        /// </summary>
        /// <value>Order total amount, made up of all items in <see cref="OrderedItems"/>.</value>
        public double OrderTotal { get => _orderTotal; }

        /// <summary>
        /// Gets the items ordered in the order.
        /// </summary>
        /// <value>Dictionary of bought books and their quantities.</value>
        public ReadOnlyDictionary<Book, int> OrderedItems => _orderedItems.AsReadOnly();
        
        public override string ToString()
        {
            StringBuilder sb = new();

            foreach(var item in OrderedItems)
            {
                sb.AppendLine($"Book title: {item.Key.Title}, Quantity: {item.Value}");
            }

            return $"Order ID: {_orderId}, \n" +
                $"Customer ID: {_customerId}, \n" +
                $"Order Date: {_orderDateTime}, \n" +
                $"Order Total: {_orderTotal}, \n" +
                $"Ordered Items: {sb}";
        }
    }
}
