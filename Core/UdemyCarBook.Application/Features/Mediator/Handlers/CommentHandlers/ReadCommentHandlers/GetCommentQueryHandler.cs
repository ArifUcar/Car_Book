using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.CommentQueries;
using UdemyCarBook.Application.Features.Mediator.Results.CommentResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.CommentHandlers.ReadCommentHandlers
{
    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, List<GetCommentQueryResult>>
    {
        private readonly IRepository<Comment> _repository;

        public GetCommentQueryHandler(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCommentQueryResult>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetAllAsync()
                .Include(x => x.News)
                .Include(x => x.ParentComment)
                .Include(x => x.Replies)
                .Include(x => x.CreatedByUser)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new GetCommentQueryResult
                {
                    Id = x.Id,
                    Content = x.Content,
                    Name = x.Name,
                    Email = x.Email,
                    IsApproved = x.IsApproved,
                    NewsId = x.NewsId,
                    NewsTitle = x.News.Title,
                    ParentCommentId = x.ParentCommentId,
                    ParentCommentContent = x.ParentComment != null ? x.ParentComment.Content : null,
                    ReplyCount = x.Replies.Count,
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.Name : null
                })
                .ToListAsync(cancellationToken);

            return comments;
        }
    }
} 