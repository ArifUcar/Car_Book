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
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand>
    {
        private readonly IRepository<News> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public CreateNewsCommandHandler(IRepository<News> repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Title))
                    throw new AuFrameWorkException("Başlık boş olamaz", "TITLE_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Content))
                    throw new AuFrameWorkException("İçerik boş olamaz", "CONTENT_REQUIRED", "ValidationError");

                var news = new News
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Content = request.Content,
                    PublishDate = request.PublishDate,
                    CategoryId = request.CategoryId,
                    AuthorId = request.AuthorId,
                    ImageUrl = request.ImageUrl,
                    Summary = request.Summary,
                    Status = request.Status,
                    ViewCount = 0,
                    IsDeleted = false
                };

                await _repository.CreateAsync(news);
                await _historyService.SaveHistory(news, "Create");
                
                await _logService.CreateLog(
                    "Haber Oluşturma",
                    $"'{request.Title}' başlıklı haber oluşturuldu",
                    "Create",
                    "News"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "NewsCreate",
                    $"Haber oluşturulurken hata: {request.Title}"
                );
                throw new AuFrameWorkException(
                    "Haber oluşturulurken bir hata oluştu", 
                    "CREATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 