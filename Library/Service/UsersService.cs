using Library.Data;
using Library.Models;

namespace Library.Service
{
    public class UsersService
    {
        private readonly LibraryContext _context;

        public UsersService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAllUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting users: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Users AddUser(Users user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding user: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user: {ex.Message}");
            }
        }
    }
}