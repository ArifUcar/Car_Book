using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly NewsContext _context;

        public AuthorRepository(NewsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllWithDetailsAsync()
        {
            return await _context.Authors
                .Include(x => x.News)
                .Include(x => x.SocialMediaAccounts)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }

        public async Task<Author> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Authors
                .Include(x => x.News)
                .Include(x => x.SocialMediaAccounts)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Author>> GetActiveAuthorsAsync()
        {
            return await _context.Authors
                .Include(x => x.News)
                .Where(x => !x.IsDeleted && x.News.Any(n => !n.IsDeleted))
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Authors
                .AnyAsync(x => x.Email == email && !x.IsDeleted);
        }

        public async Task<List<Author>> GetAuthorsByNewsCountAsync(int minNewsCount)
        {
            return await _context.Authors
                .Include(x => x.News)
                .Where(x => !x.IsDeleted && x.News.Count(n => !n.IsDeleted) >= minNewsCount)
                .OrderByDescending(x => x.News.Count(n => !n.IsDeleted))
                .ToListAsync();
        }
    }
} 