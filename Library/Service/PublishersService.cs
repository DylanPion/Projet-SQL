using Library.Data;
using Library.Models;

namespace Library.Service
{
    public class PublishersService
    {
        private readonly LibraryContext _context;

        public PublishersService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all publishers
        /// </summary>
        /// <returns></returns>
        public List<Publishers> GetAllPublishers()
        {
            try
            {
                return _context.Publishers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting publishers: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new publisher
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        public Publishers AddPublisher(Publishers publisher)
        {
            try
            {
                _context.Publishers.Add(publisher);
                _context.SaveChanges();
                return publisher;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding publisher: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a publisher if no books are associated with it
        /// </summary>
        /// <param name="id"></param>
        public void DeletePublisher(int id)
        {
            try
            {
                var publisher = _context.Publishers.Find(id);
                if (publisher == null)
                {
                    throw new Exception("Publisher not found");
                }
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting publisher: {ex.Message}");
            }
        }
    }
}