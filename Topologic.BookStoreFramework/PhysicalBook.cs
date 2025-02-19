namespace Topologic.BookStoreFramework
{
    public class PhysicalBook : Book
    {
        private double _weight;
        private int _totalPages;
        private BookCoverType _bookCoverType;

        public PhysicalBook(string isbn)
            : base(isbn)
        {
            _weight = 0;
            _totalPages = 0;
        }

        public PhysicalBook(string isbn, string title, double price, double weight, int totalPages, BookCoverType bookCoverType)
            : base(title, isbn, price)
        {
            Weight = weight;
            TotalPages = totalPages;
            BookCoverType = bookCoverType;
        }

        public PhysicalBook(string title, string isbn, double price, double weight, int totalPages, BookCoverType bookCoverType, string authorName, string description, string language, string publisher, DateTime releaseDate) 
            : base(title, isbn, price, authorName, description, language, publisher, releaseDate)
        {
            Weight = weight;
            TotalPages = totalPages;
            BookCoverType = bookCoverType;
        }

        public double Weight
        {
            get => _weight;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Weight cannot be zero or a negative");

                }
                _weight = value;
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pages cannot be zero or negative");
                }
                _totalPages = value;
            }
        }

        public BookCoverType BookCoverType
        {
            get => _bookCoverType;
            set
            {
                if (!Enum.IsDefined(value))
                {
                    throw new ArgumentNullException(nameof(value), "BookCoverType cannor be null");
                }
                if(value == BookCoverType.None)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "BookCoverType cannot be None");
                }
                _bookCoverType = value;
            }
        }
    }

}