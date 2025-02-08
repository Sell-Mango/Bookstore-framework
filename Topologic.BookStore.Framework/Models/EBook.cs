using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Models
{
    public class EBook : Book
    {
        private double _fileSize = 0;

        public EBook(string title, string isbn, double fileSize)
            : base(title, isbn)
        {
            FileSize = fileSize;
        }

        public double FileSize
        {
            get => _fileSize;
            set
            {

                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "File size cant be zero or negative");

                _fileSize = value;
            }
        }
    }
}
