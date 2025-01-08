using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Tools;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TokenResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);
            if (user == null)
                throw new AuFrameWorkException("Kullanıcı adı veya şifre hatalı", "INVALID_CREDENTIALS", "ValidationError");

           
            if (user.Password != request.Password)
                throw new AuFrameWorkException("Kullanıcı adı veya şifre hatalı", "INVALID_CREDENTIALS", "ValidationError");

            if (!user.IsActive)
                throw new AuFrameWorkException("Hesabınız aktif değil", "ACCOUNT_INACTIVE", "ValidationError");

            var token = JwtTokenGenerator.GenerateToken(user);

            user.LastLoginDate = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            return token;
        }
    }
} 