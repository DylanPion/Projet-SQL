using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    /// <summary>
    /// Class representing a loan in the library
    /// </summary>
    public class Loans
    {
        /// <summary>
        /// Constructor for the Loans class
        /// </summary>
        /// <param name="bookId">Book ID</param>
        /// <param name="borrowerName">Borrower name</param>
        /// <param name="borrowDate">Borrow date</param>
        public Loans(int bookId, int userId, DateTime borrowDate, DateTime? returnDate = null)
        {
            BookId = bookId;
            UserId = userId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
        }

        /// <summary>
        /// Unique identifier for the loan
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the Books table
        /// </summary>
        [ForeignKey("Book")]
        public int BookId { get; set; }

        /// <summary>
        /// Foreign key to the Users table
        /// Return UserName
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Borrow date (default is current date)
        /// </summary>
        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Loan date (default is current date)
        /// </summary>
        [Required]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Return date (nullable)
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}