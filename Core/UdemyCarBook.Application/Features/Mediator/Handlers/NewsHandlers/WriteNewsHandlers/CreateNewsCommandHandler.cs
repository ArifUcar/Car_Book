using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.NewsCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.NewsHandlers.WriteNewsHandlers
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, Unit>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IRepository<Tag> _tagRepository;

        public CreateNewsCommandHandler(
            INewsRepository newsRepository,
            IRepository<Tag> tagRepository)
        {
            _newsRepository = newsRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Unit> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = new News
            {
                Title = request.Title,
                Content = request.Content,
                Summary = request.Summary,
                Slug = request.Slug,
                CoverImageUrl = request.CoverImageUrl,
                IsFeatured = request.IsFeatured,
                IsActive = request.IsActive,
                IsPublished = request.IsPublished,
                PublishDate = request.PublishDate,
                MetaTitle = request.MetaTitle,
                MetaDescription = request.MetaDescription,
                MetaKeywords = request.MetaKeywords,
                CategoryId = request.CategoryId,
                AuthorId = request.AuthorId,
                CreatedDate = DateTime.UtcNow,
                CreatedById = request.CreatedById
            };

            // Tag'leri ekle
            if (request.TagIds != null)
            {
                foreach (var tagId in request.TagIds)
                {
                    var tag = await _tagRepository.GetByIdAsync(tagId);
                    if (tag != null)
                    {
                        news.Tags.Add(tag);
                    }
                }
            }

            await _newsRepository.CreateAsync(news);
            return Unit.Value;
        }
    }
} 