using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.SocialMediaQueries;
using UdemyCarBook.Application.Features.Mediator.Results.SocialMediaResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;
using System.Linq;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.SocialMediaHandlers.ReadSocialMediaHandlers
{
    public class GetSocialMediaByIdQueryHandler : IRequestHandler<GetSocialMediaByIdQuery, GetSocialMediaByIdQueryResult>
    {
        private readonly IRepository<SocialMedia> _repository;

        public GetSocialMediaByIdQueryHandler(IRepository<SocialMedia> repository)
        {
            _repository = repository;
        }

        public async Task<GetSocialMediaByIdQueryResult> Handle(GetSocialMediaByIdQuery request, CancellationToken cancellationToken)
        {
            var socialMedia = await _repository.GetAllAsync()
                .Include(x => x.Author)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Where(x => x.Id == request.Id && !x.IsDeleted)
                .Select(x => new GetSocialMediaByIdQueryResult
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
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.Name : null,
                    LastModifiedDate = x.LastModifiedDate,
                    LastModifiedByUserName = x.LastModifiedByUser != null ? x.LastModifiedByUser.Name : null
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (socialMedia == null)
                throw new AuFrameWorkException("Sosyal medya bulunamadı", "SOCIAL_MEDIA_NOT_FOUND", "NotFound");

            return socialMedia;
        }
    }
} 