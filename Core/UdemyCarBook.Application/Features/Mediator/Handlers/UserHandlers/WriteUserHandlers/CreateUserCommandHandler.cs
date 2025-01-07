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
        private readonly IUserRepository _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;
        private readonly IRoleRepository _roleRepository;

        public CreateUserCommandHandler(
            IUserRepository repository, 
            IHistoryService historyService, 
            ILogService logService,
            IRoleRepository roleRepository)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
            _roleRepository = roleRepository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserName))
                    throw new AuFrameWorkException("Kullanıcı adı boş olamaz", "USERNAME_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Password))
                    throw new AuFrameWorkException("Şifre boş olamaz", "PASSWORD_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Email))
                    throw new AuFrameWorkException("E-posta adresi boş olamaz", "EMAIL_REQUIRED", "ValidationError");

                var existingUser = await _repository.GetByUsernameAsync(request.UserName);
                if (existingUser != null)
                    throw new AuFrameWorkException("Bu kullanıcı adı zaten kullanılıyor", "USER_EXISTS", "ValidationError");

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    Password = request.Password, // TODO: Şifre hash'lenmelidir
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    UserType = request.UserType,
                    IsActive = request.IsActive,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                // Rolleri ekle
                if (request.Roles != null && request.Roles.Count > 0)
                {
                    user.Roles = new List<Role>();
                    foreach (var roleName in request.Roles)
                    {
                        var role = await _roleRepository.GetByNameAsync(roleName);
                        if (role == null)
                        {
                            role = new Role
                            {
                                Id = Guid.NewGuid(),
                                Name = roleName,
                                CreatedDate = DateTime.UtcNow,
                                CreatedById = user.Id,
                                IsDeleted = false
                            };
                            await _roleRepository.CreateAsync(role);
                        }
                        user.Roles.Add(role);
                    }
                }

                await _repository.CreateAsync(user);
                await _historyService.SaveHistory(user, "Create");
                
                await _logService.CreateLog(
                    "Kullanıcı Oluşturma",
                    $"'{request.UserName}' kullanıcı adlı kullanıcı oluşturuldu",
                    "Create",
                    "User"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "UserCreate",
                    $"Kullanıcı oluşturulurken hata: {request.UserName}"
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