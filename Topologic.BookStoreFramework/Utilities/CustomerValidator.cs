namespace Topologic.BookStoreFramework.Utilities
{
    /// <summary>
    /// Static class providing methods to validate <see cref="Customer"> data.
    /// Commonly used when creating or managing a <see cref="Customer"/> object.
    /// </summary>
    public static class CustomerValidator
    {
        /// <summary>
        /// Validates a <see cref="Customer"> email address.
        /// </summary>
        /// <param name="email">validates the provided email address.</param>
        /// <returns>True if the email address meets the criteria, otherwise false.</returns>
        public static bool IsEmailValid(string email)
        {
            if (!email.Contains('@')) return false;
            if (!email.Contains('.')) return false;
            return true;
        }
    }
}
