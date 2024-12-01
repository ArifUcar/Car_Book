using MediatR;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorsResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.ReadAuthorHandlers
{
    public class GetAuthorQueryHandler:IRequestHandler<GetAuthorQuery,List<GetAuthorQueryResults>>
    {
        private readonly IRepository<Author> _repository;

        public GetAuthorQueryHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAuthorQueryResults>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x=> new GetAuthorQueryResults
            {
                AuthorId = x.AuthorId,
                AuthorName = x.AuthorName,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
            }).ToList();
        }
    }
}
