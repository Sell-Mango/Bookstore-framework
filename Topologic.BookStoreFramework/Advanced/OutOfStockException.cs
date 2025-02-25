using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework.Advanced
{
    public class OutOfStockException: InvalidOperationException, ISerializable
    {
        public OutOfStockException()
        {
        }
        public OutOfStockException(string message)
            : base(message)
        {
        }
        public OutOfStockException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected OutOfStockException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
