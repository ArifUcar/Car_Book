using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;
using UdemyCarBook.Persistance.Repositories;


namespace UdemyCarBook.Persistence.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        private readonly NewsContext _context;

        public NewsRepository(NewsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<News> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.News
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<News>> GetAllWithDetailsAsync()
        {
            return await _context.News
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.CreatedByUser)
                .Include(x => x.Tags)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
} 