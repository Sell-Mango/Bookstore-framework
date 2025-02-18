
namespace Topologic.BookStore.Framework.Models
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
