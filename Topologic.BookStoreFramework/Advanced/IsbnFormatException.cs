using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework.Advanced
{
    public class IsbnFormatException : ArgumentException, ISerializable
    {
        public IsbnFormatException()
        {
        }
        public IsbnFormatException(string message)
            : base(message)
        {
        }
        public IsbnFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected IsbnFormatException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
