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
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
    {
        private readonly IRepository<Author> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;
        private readonly IUserService _userService;
        private readonly IPermissionAuthorizationService _permissionAuthorizationService;

        public UpdateAuthorCommandHandler(
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

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "AUTHOR_UPDATE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: AUTHOR_UPDATE, İşlem: Yazar Güncelleme",
                        "Error",
                        "Authorization"
                    );
                    throw new AuFrameWorkException("Yetkiniz yok", "PERMISSION_DENIED", "Authorization");
                }

                var author = await _repository.GetByIdAsync(request.Id);
                if (author == null)
                    throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

                author.FirstName = request.FirstName;
                author.LastName = request.LastName;
                author.Description = request.Description;
                author.ImageUrl = request.ImageUrl;
                author.Email = request.Email;
                author.IsActive = request.IsActive;
                author.LastModifiedDate = DateTime.UtcNow;
                author.LastModifiedByUserId = userId;

                await _repository.UpdateAsync(author);
                await _historyService.SaveHistory(author, "Update");

                await _logService.CreateLog(
                    "Yazar Güncellendi",
                    $"Kullanıcı ID: {userId}, Yazar ID: {request.Id}",
                    "Information",
                    "Author"
                );

                return Unit.Value;
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "UpdateAuthor",
                    "Yazar güncellenirken hata oluştu"
                );
                throw new AuFrameWorkException(
                    "Yazar güncellenirken bir hata oluştu",
                    "UPDATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 