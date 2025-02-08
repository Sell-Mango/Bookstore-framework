using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Models
{
    public enum BookActionMessage
    {
        Added,
        Removed,
        Increased,
        Decreased,
        NotFound,
        UpdateSuccess,
        UpdateFailed
    }
}
