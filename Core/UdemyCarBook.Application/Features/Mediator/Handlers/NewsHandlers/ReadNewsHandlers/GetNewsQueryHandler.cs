using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.NewsQueries;
using UdemyCarBook.Application.Features.Mediator.Results.NewsResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using System.Linq;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.NewsHandlers.ReadNewsHandlers
{
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, List<GetNewsQueryResult>>
    {
        private readonly INewsRepository _repository;

        public GetNewsQueryHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetNewsQueryResult>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
        {
            var news = await _repository.GetAllWithDetailsAsync();

            return news.Select(x => new GetNewsQueryResult
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                PublishDate = x.PublishDate,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                ImageUrl = x.ImageUrl,
                Summary = x.Summary,
                ViewCount = x.ViewCount,
                Status = x.Status,
                Tags = x.Tags.Select(t => t.Name).ToList(),
                CreatedDate = x.CreatedDate,
                CreatedByUserName = x.CreatedByUser?.Name
            }).ToList();
        }
    }
} 