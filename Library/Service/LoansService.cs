using Library.Data;
using Library.Models;

namespace Library.Service
{
    public class LoansService
    {
        private readonly LibraryContext _context;

        public LoansService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new loan
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public Loans AddLoan(Loans loan)
        {
            try
            {
                _context.Loans.Add(loan);
                _context.SaveChanges();
                return loan;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding loan: {ex.Message}");
            }
        }

        /// <summary>
        /// Save return date
        /// </summary>
        /// <param name="id"></param>
        public void SaveReturnDate(int id)
        {
            try
            {
                var loan = _context.Loans.Find(id);
                if (loan == null)
                {
                    throw new Exception("Loan not found");
                }
                loan.ReturnDate = DateTime.Now;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving return date: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all loan with filters (book, user, loan date)
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<Loans> GetAllLoans(Dictionary<string, string> filters)
        {
            try
            {
                var query = _context.Loans.AsQueryable();
                if (filters.ContainsKey("book"))
                {
                    query = query.Where(l => l.BookId == int.Parse(filters["book"]));
                }
                if (filters.ContainsKey("member"))
                {
                    query = query.Where(l => l.UserId == int.Parse(filters["member"]));
                }
                if (filters.ContainsKey("loanDate"))
                {
                    query = query.Where(l => l.LoanDate == DateTime.Parse(filters["loanDate"]));
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting loans: {ex.Message}");
            }
        }
    }
}