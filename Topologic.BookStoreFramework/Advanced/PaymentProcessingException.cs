using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework.Advanced
{
    /// <summary>
    /// The Exception that is thrown when an error occurs during payment processing.
    /// </summary>
    public class PaymentProcessingException : InvalidOperationException, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProcessingException"/> class.
        /// </summary>
        public PaymentProcessingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProcessingException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public PaymentProcessingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProcessingException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public PaymentProcessingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProcessingException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PaymentProcessingException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
