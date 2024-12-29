using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.TagCloudQueries;
using UdemyCarBook.Application.Features.Mediator.Results.TagCloudResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IncludeRepository;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.TagCloudHandlers.ReadTagCloudHandlers
{
    public class GetTagCloudByBlogQueryHandler : IRequestHandler<GetTagCloudByBlogIdQuery, List<GetTagCloudByBlogIdQueryResults>>
    {
        private readonly ITagCloudRepository _tagCloudRepository;

        public GetTagCloudByBlogQueryHandler(ITagCloudRepository tagCloudRepository)
        {
            _tagCloudRepository= tagCloudRepository;
        }

        public async Task<List<GetTagCloudByBlogIdQueryResults>> Handle(GetTagCloudByBlogIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _tagCloudRepository.GetTagCloudByBlogID(request.Id);
            return values.Select(x => new GetTagCloudByBlogIdQueryResults
            {
                BlogId = x.BlogId,
                TagCloudID = x.TagCloudID,
                TagCloudTitle = x.TagCloudTitle,

            }).ToList();
        }
    }
}
