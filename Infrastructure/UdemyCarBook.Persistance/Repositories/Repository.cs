using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NewsContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(NewsContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();  // Constructor'da initialize ediyoruz
        }
         
        public async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity); 
            
             await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                var property = entity.GetType().GetProperty("IsDeleted");
                if (property != null)
                {
                    property.SetValue(entity, true);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}

