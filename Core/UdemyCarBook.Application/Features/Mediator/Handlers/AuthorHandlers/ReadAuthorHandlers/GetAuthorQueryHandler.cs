using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.ReadAuthorHandlers
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, List<GetAuthorQueryResult>>
    {
        private readonly IAuthorRepository _repository;

        public GetAuthorQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAuthorQueryResult>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var authors = await _repository.GetAllWithDetailsAsync();

            return authors.Select(x => new GetAuthorQueryResult
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                ImageUrl = x.ImageUrl,
                Description = x.Description,
                Email = x.Email,
                NewsCount = x.News?.Count(n => !n.IsDeleted) ?? 0,
                SocialMediaCount = x.SocialMediaAccounts?.Count(s => !s.IsDeleted) ?? 0,
                CreatedDate = x.CreatedDate,
                CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.UserName : null
            }).ToList();
        }
    }
} 