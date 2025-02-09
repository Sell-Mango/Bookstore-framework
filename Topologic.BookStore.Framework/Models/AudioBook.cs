
namespace Topologic.BookStore.Framework.Models
{
    public class AudioBook : Book
    {
        private TimeSpan _duration = TimeSpan.Zero;

        public AudioBook(string title, string isbn, double price, TimeSpan duration, string narrator) 
            : base(title, isbn, price)
        {
            Duration = duration;
            Narrator = narrator;
        }
        public AudioBook(string title, string isbn, double price, TimeSpan duration, string narrator, params string[] authorNames)
            : base(title, isbn, price, authorNames)
        {
            Duration = duration;
            Narrator = narrator;
        }

        public AudioBook(string title, string isbn, double price, TimeSpan duration, string narrator, string[] authorNames, string description, string language, string publisher, DateTime releaseDate)
            : base(title, isbn, price, authorNames, description, language, publisher, releaseDate)
        {
            Duration = duration;
            Narrator = narrator;
        }

        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                if(value <= TimeSpan.Zero || value >= TimeSpan.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Audio book duration cant be lower than zero or higher {TimeSpan.MaxValue}.");
                }
                _duration = value;
            }
        }

        public string Narrator { get; set; } = string.Empty;
        
    }
}
