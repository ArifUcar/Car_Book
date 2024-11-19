using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.SocialMedias.Mediator.Queries.SocialMediaQueries;
using UdemyCarBook.Application.SocialMedias.Mediator.Results;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.SocialMedias.Mediator.Handlers.SocialMediaHandlers.ReadSocialMediaHandlers
{
    public class GetSocialMediaQueryHandler : IRequestHandler<GetSocialMediaQuery, List<GetSocialMediaQueryResults>>
    {
        private readonly IRepository<SocialMedia> _repoistory;

        public GetSocialMediaQueryHandler(IRepository<SocialMedia> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task<List<GetSocialMediaQueryResults>> Handle(GetSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var values = await _repoistory.GetAllAsync();
            return values.Select(x => new GetSocialMediaQueryResults
            {
                SocialMediaID = x.SocialMediaID,
                Name = x.Name,
                Url = x.Url,
                Icon = x.Icon,
            }).ToList();
        }
    }
}
