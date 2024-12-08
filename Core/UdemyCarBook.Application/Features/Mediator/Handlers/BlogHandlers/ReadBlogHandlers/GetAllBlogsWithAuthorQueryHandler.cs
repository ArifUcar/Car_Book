using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces.IncludeRepository;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers.ReadBlogHandlers
{
    public class GetAllBlogsWithAuthorQueryHandler:IRequestHandler<GetAllBlogsWithAuthorQuery,List<GetAllBlogsWithAuthorQueryResult>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetAllBlogsWithAuthorQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<GetAllBlogsWithAuthorQueryResult>> Handle(GetAllBlogsWithAuthorQuery request, CancellationToken cancellationToken)
        {
            var values = await _blogRepository.GetAllBlogsListWithAuthorAsync();
            return values.Select(x => new GetAllBlogsWithAuthorQueryResult
            {
                AuthorId = x.AuthorId,
                AuthorName = x.Author.AuthorName,
                BlogId = x.BlogId,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                CoverImageUrl=x.CoverImageUrl,
                CreatedDate=x.CreatedDate,
                Title=x.Title,
                AuthorDescription=x.Author.Description,
                BlogDescription=x.Description,

                
            }).ToList();

        }

     
    }
}
