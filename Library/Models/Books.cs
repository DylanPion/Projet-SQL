using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    /// <summary>
    /// Class representing a book in the library
    /// </summary>
    public class Books
    {
        /// <summary>
        /// Constructor for the Books class
        /// </summary>
        /// <param name="title">Book title</param>
        /// <param name="author">Book author</param>
        /// <param name="publishedYear">Publication year</param>
        /// <param name="isbn">Book's ISBN number</param>
        /// <param name="categoryId">Category ID</param>
        /// <param name="publisherId">Publisher ID</param>
        public Books(string title, string author, int publishedYear, string isbn, int categoryId, int publisherId)
        {
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            ISBN = isbn;
            CategoryId = categoryId;
            PublisherId = publisherId;
        }

        /// <summary>
        /// Unique identifier for the book
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Book title (required, max 200 characters)
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Book author (required, max 100 characters)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        /// <summary>
        /// Publication year (must be after 1500)
        /// </summary>
        [Range(1500, int.MaxValue, ErrorMessage = "PublishedYear must be between 1500 and the current year.")]
        public int PublishedYear { get; set; }

        /// <summary>
        /// Book's ISBN number (required format: XXX-X{1,5}-X{1,7}-X{1,7}-X)
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$", ErrorMessage = "Invalid ISBN format.")]
        public string ISBN { get; set; }

        /// <summary>
        /// Foreign key to the Categories table
        /// </summary>
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Foreign key to the Publishers table
        /// </summary>
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
    }
}