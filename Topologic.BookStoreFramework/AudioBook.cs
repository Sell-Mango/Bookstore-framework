﻿using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents an audio book, derived from <see cref="Book"/>.
    /// Can be added to the inventory of a <see cref="InventoryManager"/> and can be bought by a <see cref="Customer"/>.
    /// </summary>
    public class AudioBook : Book
    {
        private TimeSpan _duration;

        /// <summary>
        /// Creates a new instance of a <see cref="AudioBook"/> class with valid ISBN only (minimal constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the audio book, cannot be changed later.</param>
        /// <remarks>Exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public AudioBook(string isbn)
            : base(isbn)
        { 
        }

        /// <summary>
        /// Creates a new instance of an <see cref="AudioBook"/> class with basic information.
        /// </summary>
        /// <param name="isbn">A valid ISBN for the audio book, cannot be changed later.</param>
        /// <param name="title">Title of the audio book.</param>
        /// <param name="price">Price of the audio book.</param>
        /// <param name="duration">Duration of the audio book.</param>
        /// <param name="narrator">Narrator name of the audio book.</param>
        /// <remarks>Some exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public AudioBook(string isbn, string title, double price, TimeSpan duration, string narrator) 
            : base(isbn, title, price)
        {
            Duration = duration;
            Narrator = narrator;
        }

        /// <summary>
        /// Creates a new instance of an <see cref="AudioBook"/> class with all information (advanded constructor).
        /// </summary>
        /// <param name="isbn">A valid ISBN for the audio book, cannot be changed later.</param>
        /// <param name="title">Title of the audio book.</param>
        /// <param name="price">Price of the audio book.</param>
        /// <param name="duration">Duration for the audio book.</param>
        /// <param name="narrator">Narrator name of the audio book.</param>
        /// <param name="authorName">Author name of the audio book.</param>
        /// <param name="description">A brief description of the audio book (max <see cref="MAX_DESCRIPTION_LENGTH"/> characters.</param>
        /// <param name="language">Written language for the audio book.</param>
        /// <param name="publisherName">Publisher of the audio book.</param>
        /// <param name="releaseDate">Release date for the audio book.</param>
        /// <remarks>Some exceptions are handled by the base <see cref="Book"/> class.</remarks>
        public AudioBook(string isbn, string title, double price, TimeSpan duration, string narrator, string authorName, string description, string language, string publisherName, DateTime releaseDate)
            : base(title, isbn, price, authorName, description, language, publisherName, releaseDate)
        {
            Duration = duration;
            Narrator = narrator;
        }

        /// <summary>
        /// Gets or sets the duration of the audio book.
        /// </summary>
        /// <value>Duration of the audio book, in hours, minutes and seconds.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when trying to set duration to below <see cref="TimeSpan.Zero"/> 
        /// or higher than <see cref="TimeSpan.MaxValue"/>.</exception>
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                if(value <= TimeSpan.Zero || value >= TimeSpan.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Audio book duration cant be lower than zero or higher than {TimeSpan.MaxValue}.");
                }
                _duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the narrator of the audio book.
        /// </summary>
        /// <value>Narrator name of the audio book.</value>
        public string Narrator { get; set; } = string.Empty;
    }
}
