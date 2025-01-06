using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly NewsContext _context;

        public CommentRepository(NewsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Comments
                .Include(x => x.News)
                .Include(x => x.ParentComment)
                .Include(x => x.Replies)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Comment>> GetAllWithDetailsAsync()
        {
            return await _context.Comments
                .Include(x => x.News)
                .Include(x => x.ParentComment)
                .Include(x => x.Replies)
                .Include(x => x.CreatedByUser)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}

