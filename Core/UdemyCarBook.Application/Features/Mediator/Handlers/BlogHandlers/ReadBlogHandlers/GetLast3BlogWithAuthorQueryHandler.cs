using MediatR;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces.IncludeRepository;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers.ReadBlogHandlers
{
    public class GetLast3BlogWithAuthorQueryHandler : IRequestHandler<GetLast3BlogWithAuthorQuery, List<GetLast3BlogWithAuthorQueryResults>>
    {
        private readonly IBlogAuthorRepository _repository;

        public GetLast3BlogWithAuthorQueryHandler(IBlogAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLast3BlogWithAuthorQueryResults>> Handle(GetLast3BlogWithAuthorQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetLast3BlogsListWithAuthorAsync();
            return values.Select(x => new GetLast3BlogWithAuthorQueryResults
            {
                AuthorName = x.Author.AuthorName,
                AuthorId = x.Author.AuthorId,
                BlogId = x.BlogId,
                CategoryId = x.CategoryId,
                CoverImageUrl = x.CoverImageUrl,
                CreatedDate = x.CreatedDate ,
                Title=x.Title,
                Description=x.Title,
                ImageUrl=x.CoverImageUrl,
                
            }).ToList();
        }
    }
}
