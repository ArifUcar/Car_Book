using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.ReadAuthorHandlers
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, GetAuthorByIdQueryResult>
    {
        private readonly IAuthorRepository _repository;

        public GetAuthorByIdQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAuthorByIdQueryResult> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdWithDetailsAsync(request.Id);

            if (author == null)
                throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

            return new GetAuthorByIdQueryResult
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                ImageUrl = author.ImageUrl,
                Description = author.Description,
                Email = author.Email,
                News = author.News?.Where(n => !n.IsDeleted).Select(n => new AuthorNewsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    PublishDate = n.PublishDate,
                    CategoryName = n.Category?.Name
                }).ToList(),
                SocialMediaAccounts = author.SocialMediaAccounts?.Where(s => !s.IsDeleted).Select(s => new AuthorSocialMediaDto
                {
                    Id = s.Id,
                    Platform = s.Platform,
                    Url = s.Url,
                    Icon = s.Icon
                }).ToList(),
                CreatedDate = author.CreatedDate,
                CreatedByUserName = author.CreatedByUser != null ? author.CreatedByUser.UserName : null,
                LastModifiedDate = author.LastModifiedDate,
                LastModifiedByUserName = author.LastModifiedByUser != null ? author.LastModifiedByUser.UserName : null
            };
        }
    }
} 