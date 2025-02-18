namespace Topologic.BookStoreFramework
{
    public enum BookActionMessage
    {
        None = 0,
        Added,
        Removed,
        Increased,
        Decreased,
        NotFound,
        UpdateSuccess,
        UpdateFailed
    }
}
