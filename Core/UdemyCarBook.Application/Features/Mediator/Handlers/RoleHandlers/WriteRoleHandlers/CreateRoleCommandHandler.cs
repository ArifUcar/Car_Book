using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.RoleCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.RoleHandlers.WriteRoleHandlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public CreateRoleCommandHandler(IRoleRepository repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                    throw new AuFrameWorkException("Rol adı boş olamaz", "NAME_REQUIRED", "ValidationError");

                var isRoleNameExists = await _repository.IsRoleNameExistsAsync(request.Name);
                if (isRoleNameExists)
                    throw new AuFrameWorkException("Bu rol adı zaten kullanılıyor", "ROLE_NAME_EXISTS", "ValidationError");

                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    IsDeleted = false
                };

                await _repository.CreateAsync(role);
                await _historyService.SaveHistory(role, "Create");
                
                await _logService.CreateLog(
                    "Rol Oluşturma",
                    $"'{request.Name}' adlı rol oluşturuldu",
                    "Create",
                    "Role"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "RoleCreate",
                    $"Rol oluşturulurken hata: {request.Name}"
                );
                throw new AuFrameWorkException(
                    "Rol oluşturulurken bir hata oluştu", 
                    "CREATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 