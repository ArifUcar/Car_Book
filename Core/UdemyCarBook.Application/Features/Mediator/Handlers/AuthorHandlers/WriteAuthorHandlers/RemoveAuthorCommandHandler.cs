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
    public class RemoveAuthorCommandHandler : IRequestHandler<RemoveAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public RemoveAuthorCommandHandler(IAuthorRepository repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _repository.GetByIdWithDetailsAsync(request.Id);
                if (author == null)
                    throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

                if (author.News != null && author.News.Any())
                    throw new AuFrameWorkException("Bu yazarın haberleri var. Önce haberleri başka bir yazara atayın veya silin.", "AUTHOR_HAS_NEWS", "ValidationError");

                await _repository.RemoveAsync(author);
                await _historyService.SaveHistory(author, "Remove");
                
                await _logService.CreateLog(
                    "Yazar Silme",
                    $"'{author.FirstName} {author.LastName}' adlı yazar silindi",
                    "Remove",
                    "Author"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "AuthorRemove",
                    $"Yazar silinirken hata: {request.Id}"
                );
                throw new AuFrameWorkException(
                    "Yazar silinirken bir hata oluştu", 
                    "REMOVE_ERROR",
                    "Error"
                );
            }
        }
    }
} 