using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.TagQueries;
using UdemyCarBook.Application.Features.Mediator.Results.TagResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;
using System.Linq;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.TagHandlers.ReadTagHandlers
{
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, GetTagByIdQueryResult>
    {
        private readonly IRepository<Tag> _repository;

        public GetTagByIdQueryHandler(IRepository<Tag> repository)
        {
            _repository = repository;
        }

        public async Task<GetTagByIdQueryResult> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetAll()
                .Include(x => x.News)
                .ThenInclude(x => x.Author)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Where(x => x.Id == request.Id && !x.IsDeleted)
                .Select(x => new GetTagByIdQueryResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    News = x.News.Select(n => new TagNewsDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        AuthorName = n.Author.Name,
                        PublishDate = n.PublishDate
                    }).ToList(),
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.Name : null,
                    LastModifiedDate = x.LastModifiedDate,
                    LastModifiedByUserName = x.LastModifiedByUser != null ? x.LastModifiedByUser.Name : null
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (tag == null)
                throw new AuFrameWorkException("Etiket bulunamadı", "TAG_NOT_FOUND", "NotFound");

            return tag;
        }
    }
} 