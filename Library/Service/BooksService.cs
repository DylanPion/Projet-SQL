using Library.Data;
using Library.Models;

namespace Library.Service
{
    public class BooksService
    {
        private readonly LibraryContext _context;

        public BooksService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a book by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetBookById(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book == null)
                {
                    throw new Exception("Book not found");
                }
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting book: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Books AddBook(Books book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding book: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Books UpdateBook(Books book)
        {
            try
            {
                _context.Books.Update(book);
                _context.SaveChanges();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating book: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a book if isn't borrowed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteBook(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book == null)
                {
                    throw new Exception("Book not found");
                }
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting book: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all books (possible with filters)
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<Books> GetAllBooks(Dictionary<string, string> filters)
        {
            try
            {
                var query = _context.Books.AsQueryable();
                if (filters.ContainsKey("title"))
                {
                    query = query.Where(b => b.Title.Contains(filters["title"]));
                }
                if (filters.ContainsKey("author"))
                {
                    query = query.Where(b => b.Author.Contains(filters["author"]));
                }
                if (filters.ContainsKey("publisher"))
                {
                    query = query.Where(b => b.PublisherId == int.Parse(filters["publisher"]));
                }
                if (filters.ContainsKey("category"))
                {
                    query = query.Where(b => b.CategoryId == int.Parse(filters["category"]));
                }
                if (filters.ContainsKey("publishedYear"))
                {
                    query = query.Where(b => b.PublishedYear == int.Parse(filters["publishedYear"]));
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting books: {ex.Message}");
            }
        }
    }
}
