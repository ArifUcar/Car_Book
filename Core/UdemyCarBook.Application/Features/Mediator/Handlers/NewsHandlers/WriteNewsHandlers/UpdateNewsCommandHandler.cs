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
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand>
    {
        private readonly IRepository<News> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public UpdateNewsCommandHandler(IRepository<News> repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var news = await _repository.GetByIdAsync(request.Id);
                if (news == null)
                    throw new AuFrameWorkException("Haber bulunamadı", "NEWS_NOT_FOUND", "NotFound");

                if (string.IsNullOrEmpty(request.Title))
                    throw new AuFrameWorkException("Başlık boş olamaz", "TITLE_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Content))
                    throw new AuFrameWorkException("İçerik boş olamaz", "CONTENT_REQUIRED", "ValidationError");

                news.Title = request.Title;
                news.Content = request.Content;
                news.PublishDate = request.PublishDate;
                news.CategoryId = request.CategoryId;
                news.AuthorId = request.AuthorId;
                news.ImageUrl = request.ImageUrl;
                news.Summary = request.Summary;
                news.Status = request.Status;
                news.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(news);
                await _historyService.SaveHistory(news, "Update");
                
                await _logService.CreateLog(
                    "Haber Güncelleme",
                    $"'{request.Title}' başlıklı haber güncellendi",
                    "Update",
                    "News"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "NewsUpdate",
                    $"Haber güncellenirken hata: {request.Title}"
                );
                throw new AuFrameWorkException(
                    "Haber güncellenirken bir hata oluştu", 
                    "UPDATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 