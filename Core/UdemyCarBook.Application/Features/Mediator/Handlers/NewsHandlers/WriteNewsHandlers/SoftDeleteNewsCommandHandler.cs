using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.NewsCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.NewsHandlers.WriteNewsHandlers
{
    public class SoftDeleteNewsCommandHandler : IRequestHandler<SoftDeleteNewsCommand>
    {
        private readonly IRepository<News> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public SoftDeleteNewsCommandHandler(IRepository<News> repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(SoftDeleteNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var news = await _repository.GetByIdAsync(request.Id);
                if (news == null)
                    throw new AuFrameWorkException("Haber bulunamadı", "NEWS_NOT_FOUND", "NotFound");

                news.IsDeleted = true;
                news.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(news);
                await _historyService.SaveHistory(news, "SoftDelete");
                
                await _logService.CreateLog(
                    "Haber Yumuşak Silme",
                    $"'{news.Title}' başlıklı haber yumuşak silindi",
                    "SoftDelete",
                    "News"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "NewsSoftDelete",
                    $"Haber yumuşak silinirken hata: {request.Id}"
                );
                throw new AuFrameWorkException(
                    "Haber yumuşak silinirken bir hata oluştu", 
                    "SOFT_DELETE_ERROR",
                    "Error"
                );
            }
        }
    }
} 