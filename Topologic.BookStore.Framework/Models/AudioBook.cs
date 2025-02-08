using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStore.Framework.Models
{
    public class AudioBook : Book
    {
        private int _numOfCopies;
        private TimeSpan _duration = TimeSpan.Zero;

        public AudioBook(string title, string isbn, int numOfCopies, TimeSpan duration, string narrator) 
            : base(title, isbn)
        {
            NumOfCopies = numOfCopies;
            Duration = duration;
            Narrator = narrator;
        }
        public AudioBook(string title, string isbn, decimal price, int numOfCopies, TimeSpan duration, string narrator, params string[] authorNames)
            : base(title, isbn, price, authorNames)
        {
            NumOfCopies = numOfCopies;
            Duration = duration;
            Narrator = narrator;
        }

        public AudioBook(string title, string isbn, decimal price, int numOfCopies, TimeSpan duration, string narrator, string[] authorNames, string description, string language, string publisher, DateTime releaseDate)
            : base(title, isbn, price, authorNames, description, language, publisher, releaseDate)
        {
            NumOfCopies = numOfCopies;
            Duration = duration;
            Narrator = narrator;
        }

        public int NumOfCopies
        {
            get => _numOfCopies;
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Number of copies cannot be negative");
                _numOfCopies = value;
            }
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
