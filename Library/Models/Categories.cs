using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    /// <summary>
    /// Class representing a category in the library
    /// </summary>
    public class Categories
    {
        /// <summary>
        /// Constructor for the Categories class
        /// </summary>
        /// <param name="name">Category name</param>
        public Categories(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Unique identifier for the category
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Category name (required, max 50 characters)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}