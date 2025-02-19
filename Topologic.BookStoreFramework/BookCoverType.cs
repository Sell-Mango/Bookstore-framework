using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents the form or bindings of a <see cref="PhysicalBook"/>.
    /// </summary>
    public enum BookCoverType
    {
        /// <summary>
        /// No cover type or undefined status, not allowed in this context.
        /// </summary>
        None = 0,

        /// <summary>
        /// Hardcover binding.
        /// </summary>
        Hardcover,
        
        /// <summary>
        /// Paperback binding.
        /// </summary>
        Paperback,

        /// <summary>
        /// Leatherbound binding.
        /// </summary>
        Leatherbound,

        /// <summary>
        /// Magazine binding.
        /// </summary>
        Magazine,

        /// <summary>
        /// Spiral binding.
        /// </summary>
        Spiral,

        /// <summary>
        /// Comicbook form.
        /// </summary>
        Comicbook,

        /// <summary>
        /// Coloringbook form.
        /// </summary>
        Coloringbook
    }
}
