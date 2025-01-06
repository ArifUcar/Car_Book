using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
    {
        private readonly NewsContext _context;

        public NewsletterRepository(NewsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Newsletter>> GetAllWithDetailsAsync()
        {
            return await _context.Newsletters
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreationTime)
                .ToListAsync();
        }

        public async Task<Newsletter> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Newsletters
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Newsletter>> GetActiveSubscribersAsync()
        {
            return await _context.Newsletters
                .Include(x => x.CreatedByUser)
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderByDescending(x => x.SubscriptionDate)
                .ToListAsync();
        }

        public async Task<bool> IsEmailSubscribedAsync(string email)
        {
            return await _context.Newsletters
                .AnyAsync(x => x.Email == email && !x.IsDeleted && x.IsActive);
        }
    }
} 