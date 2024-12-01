using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers.ReadBlogHandlers
{
    public class GetBlogByIdQueryHandler:IRequestHandler<GetBlogByIdQuery, GetBlogByIdQueryResults>
    {
        private readonly IRepository<Blog> _repository;

        public GetBlogByIdQueryHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<GetBlogByIdQueryResults> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {

            var values = await _repository.GetByIdAsync(request.Id);
            return new GetBlogByIdQueryResults {
            BlogId=values.BlogId,
            Title=values.Title
            };
        }
    }
    }

