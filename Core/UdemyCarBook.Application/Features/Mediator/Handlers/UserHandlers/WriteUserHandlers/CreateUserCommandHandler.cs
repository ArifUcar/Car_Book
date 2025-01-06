using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.UserCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.UserHandlers.WriteUserHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public CreateUserCommandHandler(IRepository<User> repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Username))
                    throw new AuFrameWorkException("Kullanıcı adı boş olamaz", "USERNAME_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Password))
                    throw new AuFrameWorkException("Şifre boş olamaz", "PASSWORD_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Email))
                    throw new AuFrameWorkException("E-posta adresi boş olamaz", "EMAIL_REQUIRED", "ValidationError");

                var existingUser = await _repository.GetFirstOrDefaultAsync(x => x.Username == request.Username || x.Email == request.Email);
                if (existingUser != null)
                    throw new AuFrameWorkException("Bu kullanıcı adı veya e-posta adresi zaten kullanılıyor", "USER_EXISTS", "ValidationError");

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Password = request.Password, // Şifre hash'lenmelidir
                    Email = request.Email,
                    UserType = request.UserType,
                    IsDeleted = false
                };

                await _repository.CreateAsync(user);
                await _historyService.SaveHistory(user, "Create");
                
                await _logService.CreateLog(
                    "Kullanıcı Oluşturma",
                    $"'{request.Username}' kullanıcı adlı kullanıcı oluşturuldu",
                    "Create",
                    "User"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "UserCreate",
                    $"Kullanıcı oluşturulurken hata: {request.Username}"
                );
                throw new AuFrameWorkException(
                    "Kullanıcı oluşturulurken bir hata oluştu", 
                    "CREATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 