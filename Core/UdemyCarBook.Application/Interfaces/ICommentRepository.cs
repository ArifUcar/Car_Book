using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> GetByIdWithDetailsAsync(Guid id);
        Task<List<Comment>> GetAllWithDetailsAsync();
    }
} 