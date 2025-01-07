using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

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
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new AuFrameWorkException("Oturum açmış kullanıcı bulunamadı", "USER_NOT_FOUND", "NotFound");

            var user = await _userRepository.GetByIdWithDetailsAsync(Guid.Parse(userId));
            if (user == null)
                throw new AuFrameWorkException("Kullanıcı bulunamadı", "USER_NOT_FOUND", "NotFound");

            return user;
        }
    }
} 