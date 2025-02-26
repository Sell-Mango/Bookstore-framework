using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework.Advanced
{
    public class PaymentProcessingException : InvalidOperationException, ISerializable
    {
        public PaymentProcessingException()
        {
        }

        public PaymentProcessingException(string message)
            : base(message)
        {
        }

        public PaymentProcessingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected PaymentProcessingException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
