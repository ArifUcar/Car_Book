using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.WriteAuthorHandlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Unit>
    {
        private readonly IRepository<Author> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;
        private readonly IUserService _userService;
        private readonly IPermissionAuthorizationService _permissionAuthorizationService;

        public CreateAuthorCommandHandler(
            IRepository<Author> repository,
            IHistoryService historyService,
            ILogService logService,
            IUserService userService,
            IPermissionAuthorizationService permissionAuthorizationService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
            _userService = userService;
            _permissionAuthorizationService = permissionAuthorizationService;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "AUTHOR_CREATE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: AUTHOR_CREATE, İşlem: Yazar Oluşturma",
                        "Error",
                        "Authorization"
                    );
                    throw new AuFrameWorkException("Yetkiniz yok", "PERMISSION_DENIED", "Authorization");
                }

                var author = new Author
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    Email = request.Email,
                    IsActive = request.IsActive,
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = userId,
                    LastModifiedDate = DateTime.UtcNow,
                    LastModifiedByUserId = userId
                };

                await _repository.CreateAsync(author);
                await _historyService.SaveHistory(author, "Create");

                await _logService.CreateLog(
                    "Yazar Oluşturuldu",
                    $"Kullanıcı ID: {userId}, Yazar: {request.FirstName} {request.LastName}",
                    "Information",
                    "Author"
                );

                return Unit.Value;
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "CreateAuthor",
                    "Yazar oluşturulurken hata oluştu"
                );
                throw new AuFrameWorkException(
                    "Yazar oluşturulurken bir hata oluştu",
                    "CREATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 