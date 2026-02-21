using Microsoft.EntityFrameworkCore;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;
namespace WebApplication4.Infrastructure.Repository
{
    public class FeedBackRepo : IFeedBackRepo
    {
        private readonly ApplicationDbContext _context;
        public FeedBackRepo(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(PatientFeedback feedBack)
        {
           await _context.PatientsFeedback.AddAsync(feedBack);
        }

        public async Task<List<PatientFeedback>> GetAllAsync()
        {
            return await _context.PatientsFeedback.OrderByDescending(f => f.CreatedAt).ToListAsync();
        }
        public async Task<PatientFeedback?> GetByIdAsync(int id)
        {
            return await _context.PatientsFeedback.FindAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var feedBack = await _context.PatientsFeedback.FindAsync(id);
            if (feedBack != null)
            {
                _context.PatientsFeedback.Remove(feedBack);
            }
        }
    }
}
