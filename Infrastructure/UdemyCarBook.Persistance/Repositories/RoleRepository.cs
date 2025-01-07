using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly NewsContext _context;

        public RoleRepository(NewsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllWithDetailsAsync()
        {
            return await _context.Roles
                .Include(x => x.Users)
                .Include(x => x.CreatedByUser)
               .Include(x => x.UpdatedByUser)  // Eklenmeli
                .Include(x => x.LastModifiedByUser)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Role> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Roles
                .Include(x => x.Users)
                .Include(x => x.CreatedByUser)
                 .Include(x => x.UpdatedByUser)  // Eklenmeli
                .Include(x => x.LastModifiedByUser)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(Guid userId)
        {
            return await _context.Roles
                .Include(x => x.Users)
                .Where(x => x.Users.Any(u => u.Id == userId) && !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<bool> IsRoleNameExistsAsync(string name)
        {
            return await _context.Roles
                .AnyAsync(x => x.Name == name && !x.IsDeleted);
        }
    }
} 