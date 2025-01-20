using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.ReadAuthorHandlers
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, List<GetAuthorQueryResult>>
    {
        private readonly IAuthorRepository _repository;
        private readonly ILogService _logService;
        private readonly IUserService _userService;
        private readonly IPermissionAuthorizationService _permissionAuthorizationService;

        public GetAuthorQueryHandler(
            IAuthorRepository repository,
            ILogService logService,
            IUserService userService,
            IPermissionAuthorizationService permissionAuthorizationService)
        {
            _repository = repository;
            _logService = logService;
            _userService = userService;
            _permissionAuthorizationService = permissionAuthorizationService;
        }

        public async Task<List<GetAuthorQueryResult>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (userId == Guid.Empty)
                {
                    await _logService.CreateLog(
                        "Kullanıcı Hatası",
                        "Oturum açmış kullanıcı bulunamadı",
                        "Error",
                        "Authorization"
                    );
                    throw new AuFrameWorkException("Oturum açmış kullanıcı bulunamadı", "USER_NOT_FOUND", "Authorization");
                }

                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "AUTHOR_VIEW"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: AUTHOR_VIEW, İşlem: Yazar Listeleme",
                        "Error",
                        "Authorization"
                    );
                    throw new AuFrameWorkException("Yetkiniz yok", "PERMISSION_DENIED", "Authorization");
                }

                var authors = await _repository.GetAllWithDetailsAsync();
                if (authors == null)
                {
                    await _logService.CreateLog(
                        "Veri Hatası",
                        "Yazarlar listesi null döndü",
                        "Error",
                        "Data"
                    );
                    throw new AuFrameWorkException("Yazarlar listesi alınamadı", "DATA_ERROR", "Error");
                }

                var result = authors.Select(x => new GetAuthorQueryResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName ?? "",
                    LastName = x.LastName ?? "",
                    Email = x.Email ?? "",
                    Description = x.Description ?? "",
                    ImageUrl = x.ImageUrl ?? "",
                    CreatedById = x.CreatedById,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    NewsCount = x.News?.Count(n => !n.IsDeleted) ?? 0,
                    SocialMediaCount = x.SocialMediaAccounts?.Count(s => !s.IsDeleted) ?? 0,
                    CreatedByUserName = x.CreatedByUser?.UserName,
                    UpdatedByUserName = x.UpdatedByUser?.UserName,
                    LastModifiedByUserName = x.LastModifiedByUser?.UserName,
                    LastModifiedDate = x.LastModifiedDate,
                    UpdatedByUserId = x.UpdatedByUserId,
                    LastModifiedByUserId = x.LastModifiedByUserId
                }).ToList();

                await _logService.CreateLog(
                    "Yazarlar Listelendi",
                    $"Kullanıcı ID: {userId}, Toplam Yazar: {result.Count}",
                    "Information",
                    "Author"
                );

                return result;
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                var errorMessage = $"Yazarlar listelenirken hata oluştu: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                await _logService.CreateErrorLog(
                    ex,
                    "GetAuthors",
                    errorMessage
                );

                throw new AuFrameWorkException(
                    errorMessage,
                    "LIST_ERROR",
                    "Error"
                );
            }
        }
    }
} 