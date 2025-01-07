using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.NewsQueries;
using UdemyCarBook.Application.Features.Mediator.Results.NewsResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;
using System.Linq;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.NewsHandlers.ReadNewsHandlers
{
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, GetNewsByIdQueryResult>
    {
        private readonly INewsRepository _repository;

        public GetNewsByIdQueryHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetNewsByIdQueryResult> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var news = await _repository.GetByIdWithDetailsAsync(request.Id);

            if (news == null)
                throw new AuFrameWorkException("Haber bulunamadı", "NEWS_NOT_FOUND", "NotFound");

            return new GetNewsByIdQueryResult
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                PublishDate = news.PublishDate,
                CategoryId = news.CategoryId,
                CategoryName = news.Category.Name,
                AuthorId = news.AuthorId,
                AuthorName = news.Author.FirstName,
                ImageUrl = news.ImageUrl,
                Summary = news.Summary,
                ViewCount = news.ViewCount,
                Status = news.Status,
                Tags = news.Tags.Select(t => t.Name).ToList(),
                CreatedDate = news.CreatedDate,
                CreatedByUserName = news.CreatedByUser?.UserName,
                LastModifiedDate = news.LastModifiedDate,
                LastModifiedByUserName = news.LastModifiedByUser?.UserName
            };
        }
    }
} 