using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryContext : DbContext
    {
        /// <summary>
        /// Properties representing the database tables  
        /// </summary>
        public DbSet<Books> Books { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// Constructor that takes configuration options (useful for database configuration)
        /// </summary>
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configuration method to customize the model or add additional configurations
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}