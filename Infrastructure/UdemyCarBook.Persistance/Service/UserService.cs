using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Enums;

namespace UdemyCarBook.Persistance.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            // Geçici olarak sabit bir kullanıcı oluşturuyoruz
            // TODO: JWT token'dan kullanıcı bilgisini al
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                UserType = UserType.SuperAdmin,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            return user;
        }
    }
} 