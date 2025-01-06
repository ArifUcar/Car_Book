using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<Author>> GetAllWithDetailsAsync();
        Task<Author> GetByIdWithDetailsAsync(Guid id);
        Task<List<Author>> GetActiveAuthorsAsync();
        Task<bool> IsEmailExistsAsync(string email);
        Task<List<Author>> GetAuthorsByNewsCountAsync(int minNewsCount);
    }
} 