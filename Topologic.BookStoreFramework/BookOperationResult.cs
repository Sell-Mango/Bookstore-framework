namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a result of an action perfomed on a collection of <see cref="Book"/> objects. 
    /// Commonly used on <see cref="InventoryManager"/> and <see cref="ShoppingCart"/> operations.
    /// </summary>
    public enum BookOperationResult
    {
        /// <summary>
        /// No action or undefined status, not allowed in this context.
        /// </summary>
        None = 0,

        /// <summary>
        /// A new <see cref="Book"/> was added successfully added to a collection.
        /// </summary>
        Added,
        Removed,

        /// <summary>
        /// The quantity of a <see cref="Book"/> was increased.
        /// </summary>
        Increased,

        /// <summary>
        /// The quantity of a <see cref="Book"/> was decreased.
        /// </summary>
        Decreased,

        /// <summary>
        /// The <see cref="Book"/> was not found in the collection.
        /// </summary>
        NotFound,

        /// <summary>
        /// The <see cref="Book"/> was updated successfully in a collection.
        /// </summary>
        UpdateSuccess,

        /// <summary>
        /// The update of a <see cref="Book"/> failed in a collection.
        /// </summary>
        UpdateFailed
    }
}
