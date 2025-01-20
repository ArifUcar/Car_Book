using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly NewsContext _context;

        public AuthorRepository(NewsContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Author entity)
        {
            await _context.Authors.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Author>> GetActiveAuthorsAsync()
        {
            return await _context.Authors
                .Include(x => x.News.Where(n => !n.IsDeleted))
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Include(x => x.SocialMediaAccounts.Where(s => !s.IsDeleted))
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .Include(x => x.News.Where(n => !n.IsDeleted))
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Include(x => x.SocialMediaAccounts.Where(s => !s.IsDeleted))
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Author>> GetAllWithDetailsAsync()
        {
            return await _context.Authors
                .Include(x => x.News.Where(n => !n.IsDeleted))
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Include(x => x.SocialMediaAccounts.Where(s => !s.IsDeleted))
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public Task<List<Author>> GetAuthorsByNewsCountAsync(int minNewsCount)
        {
            throw new NotImplementedException();
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Authors
                .Include(x => x.News.Where(n => !n.IsDeleted))
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Include(x => x.SocialMediaAccounts.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public Task<Author> GetFirstOrDefaultAsync(Expression<Func<Author, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Authors
                .AnyAsync(x => x.Email == email && !x.IsDeleted);
        }

        public Task RemoveAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Author entity)
        {
            _context.Authors.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
} 