using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.NewsCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.NewsHandlers.WriteNewsHandlers
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, Unit>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IRepository<Tag> _tagRepository;

        public UpdateNewsCommandHandler(
            INewsRepository newsRepository,
            IRepository<Tag> tagRepository)
        {
            _newsRepository = newsRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Unit> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id);
            if (news == null)
                throw new AuFrameWorkException("Haber bulunamadı", "NEWS_NOT_FOUND", "NotFound");

            news.Title = request.Title;
            news.Content = request.Content;
            news.Summary = request.Summary;
            news.Slug = request.Slug;
            news.CoverImageUrl = request.CoverImageUrl;
            news.IsFeatured = request.IsFeatured;
            news.IsActive = request.IsActive;
            news.IsPublished = request.IsPublished;
            news.PublishDate = request.PublishDate;
            news.MetaTitle = request.MetaTitle;
            news.MetaDescription = request.MetaDescription;
            news.MetaKeywords = request.MetaKeywords;
            news.CategoryId = request.CategoryId;
            news.AuthorId = request.AuthorId;
            news.LastModifiedDate = DateTime.UtcNow;
            news.LastModifiedByUserId = request.LastModifiedById;

            // Tag'leri güncelle
            news.Tags.Clear();
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

            await _newsRepository.UpdateAsync(news);
            return Unit.Value;
        }
    }
} 