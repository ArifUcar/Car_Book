using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<User> GetCurrentUserAsync();
    }
} 