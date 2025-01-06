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
    public class SoftDeleteAuthorCommandHandler : IRequestHandler<SoftDeleteAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public SoftDeleteAuthorCommandHandler(IAuthorRepository repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(SoftDeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _repository.GetByIdWithDetailsAsync(request.Id);
                if (author == null)
                    throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

                if (author.News != null && author.News.Any())
                    throw new AuFrameWorkException("Bu yazarın haberleri var. Önce haberleri başka bir yazara atayın veya silin.", "AUTHOR_HAS_NEWS", "ValidationError");

                author.IsDeleted = true;
                author.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(author);
                await _historyService.SaveHistory(author, "SoftDelete");
                
                await _logService.CreateLog(
                    "Yazar Yumuşak Silme",
                    $"'{author.Name} {author.Surname}' adlı yazar yumuşak silindi",
                    "SoftDelete",
                    "Author"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "AuthorSoftDelete",
                    $"Yazar yumuşak silinirken hata: {request.Id}"
                );
                throw new AuFrameWorkException(
                    "Yazar yumuşak silinirken bir hata oluştu", 
                    "SOFT_DELETE_ERROR",
                    "Error"
                );
            }
        }
    }
} 