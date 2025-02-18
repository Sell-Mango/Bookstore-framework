namespace Topologic.BookStoreFramework
{
    public class PhysicalBook : Book
    {
        private double _weight;
        private int _totalPages;

        public PhysicalBook(string title, string isbn, double price, double weight, int totalPages)
            : base(title, isbn, price)
        {
            _weight = weight;
            _totalPages = totalPages;
        }


        public PhysicalBook(string title, string isbn, double price, double weight, int totalPages, string authorName) 
            : base(title, isbn, price, authorName)
        {
            Weight = weight;
            TotalPages = totalPages;
            
        }

        public PhysicalBook(string title, string isbn, double price, double weight, int totalPages, string authorName, string description, string language, string publisher, DateTime releaseDate) 
            : base(title, isbn, price, authorName, description, language, publisher, releaseDate)
        {
            Weight = weight;
            TotalPages = totalPages;
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
    }

}