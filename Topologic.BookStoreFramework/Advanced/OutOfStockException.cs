using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework.Advanced
{
    /// <summary>
    /// The Exception that is thrown when the stock of a product is either empty or not enough to fulfill the order.
    /// </summary>
    public class OutOfStockException: InvalidOperationException, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutOfStockException"/> class.
        /// </summary>
        public OutOfStockException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutOfStockException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public OutOfStockException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutOfStockException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public OutOfStockException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutOfStockException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected OutOfStockException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
