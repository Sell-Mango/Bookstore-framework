
namespace Topologic.BookStoreFramework
{
    public class EBook : Book
    {
        private double _fileSize = 0;

        public EBook(string title, string isbn, double price, double fileSize)
            : base(title, isbn, price)
        {
            FileSize = fileSize;
        }

        public EBook(string title, string isbn, double fileSize, double price, string authorName)
            : base(title, isbn, price, authorName)
        {
            FileSize = fileSize;
        }

        public EBook(string title, string isbn, double fileSize, double price, string authorName, string description, string language, string publisher, DateTime releaseDate)
            : base(title, isbn, price, authorName, description, language, publisher, releaseDate)
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
