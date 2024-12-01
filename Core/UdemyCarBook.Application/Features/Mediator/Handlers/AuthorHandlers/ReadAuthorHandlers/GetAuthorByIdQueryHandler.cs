using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorsResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.ReadAuthorHandlers
{
    public class GetAuthorByIdQueryHandler :IRequestHandler<GetAuthorByIdQuery,GetAuthorByIdQueryResults>
    {
        private readonly IRepository<Author> _repository;

        public GetAuthorByIdQueryHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<GetAuthorByIdQueryResults> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
           var values= await _repository.GetByIdAsync(request.Id);
            return new GetAuthorByIdQueryResults
            {
                AuthorId = values.AuthorId,
                AuthorName = values.AuthorName,
                Description = values.Description,
                ImageUrl = values.ImageUrl,
            };                            
        }
    }
}
