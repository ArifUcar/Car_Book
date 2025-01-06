using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.SocialMediaQueries;
using UdemyCarBook.Application.Features.Mediator.Results.SocialMediaResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.SocialMediaHandlers.ReadSocialMediaHandlers
{
    public class GetSocialMediaQueryHandler : IRequestHandler<GetSocialMediaQuery, List<GetSocialMediaQueryResult>>
    {
        private readonly IRepository<SocialMedia> _repository;

        public GetSocialMediaQueryHandler(IRepository<SocialMedia> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetSocialMediaQueryResult>> Handle(GetSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var socialMedias = await _repository.GetAll()
                .Include(x => x.Author)
                .Include(x => x.CreatedByUser)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new GetSocialMediaQueryResult
                {
                    Id = x.Id,
                    Platform = x.Platform,
                    Url = x.Url,
                    Icon = x.Icon,
                    DisplayOrder = x.DisplayOrder,
                    IsActive = x.IsActive,
                    FollowerCount = x.FollowerCount,
                    AccountName = x.AccountName,
                    AuthorId = x.AuthorId,
                    AuthorName = x.Author != null ? x.Author.Name : null,
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.Name : null
                })
                .ToListAsync(cancellationToken);

            return socialMedias;
        }
    }
} 