using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class GetBlogByAuthorIdQueryHandler:IRequestHandler<GetBlogByAuthorIdQuery,GetBlogByAuthorIdQueryResult>
    {
        private readonly IBlogRepository _repository;

        public GetBlogByAuthorIdQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetBlogByAuthorIdQueryResult> Handle(GetBlogByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetBlogByAuthorIdAsync(request.Id);
            var result = value.FirstOrDefault(); 

         
            return new GetBlogByAuthorIdQueryResult
            {
                AuthorId = result.AuthorId,
                BlogId = result.BlogId,
                AuthorName = result.Author.AuthorName,
                AuthorDescription = result.Author.Description,
                AuthorImageUrl = result.Author.ImageUrl
            };
        }


    }
}

