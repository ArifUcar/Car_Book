using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        Task<News> GetByIdWithDetailsAsync(Guid id);
        Task<List<News>> GetAllWithDetailsAsync();
    }
} 