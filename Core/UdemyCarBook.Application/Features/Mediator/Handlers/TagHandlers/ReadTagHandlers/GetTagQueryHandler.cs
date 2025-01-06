using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.TagQueries;
using UdemyCarBook.Application.Features.Mediator.Results.TagResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.TagHandlers.ReadTagHandlers
{
    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, List<GetTagQueryResult>>
    {
        private readonly IRepository<Tag> _repository;

        public GetTagQueryHandler(IRepository<Tag> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetTagQueryResult>> Handle(GetTagQuery request, CancellationToken cancellationToken)
        {
            var tags = await _repository.GetAll()
                .Include(x => x.News)
                .Include(x => x.CreatedByUser)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new GetTagQueryResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    NewsNames = x.News.Select(n => n.Title).ToList(),
                    NewsCount = x.News.Count,
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.Name : null
                })
                .ToListAsync(cancellationToken);

            return tags;
        }
    }
} 