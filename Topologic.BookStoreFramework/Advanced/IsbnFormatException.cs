using System.Runtime.Serialization;

namespace Topologic.BookStoreFramework.Advanced
{
    /// <summary>
    /// The Exception that is thrown when the givesn ISBN does not match the accepted format.
    /// </summary>
    public class IsbnFormatException : ArgumentException, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsbnFormatException"/> class.
        /// </summary>
        public IsbnFormatException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsbnFormatException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public IsbnFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsbnFormatException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public IsbnFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="IsbnFormatException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected IsbnFormatException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
