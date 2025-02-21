using Library.Data;
using Library.Models;

namespace Library.Service
{
    public class CategoriesService
    {
        private readonly LibraryContext _context;

        public CategoriesService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetAllCategories()
        {
            try
            {
                return _context.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting categories: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Categories AddCategory(Categories category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding category: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a category if no books are associated with it
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting category: {ex.Message}");
            }
        }
    }
}