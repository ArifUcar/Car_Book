using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.UserQueries;
using UdemyCarBook.Application.Features.Mediator.Results.UserResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;
using System.Linq;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.UserHandlers.ReadUserHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
    {
        private readonly IRepository<User> _repository;

        public GetUserByIdQueryHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllAsync()
                .Include(x => x.Roles)
                .Include(x => x.CreatedByUser)
                .Include(x => x.LastModifiedByUser)
                .Include(x => x.CommentsCreatedBy)
                .Include(x => x.CreatedNews)
                .Include(x => x.UpdatedNews)
                .Where(x => x.Id == request.Id && !x.IsDeleted)
                .Select(x => new GetUserByIdQueryResult
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    UserType = x.UserType,
                    RoleNames = x.Roles.Select(r => r.Name).ToList(),
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser.Name,
                    LastModifiedDate = x.LastModifiedDate,
                    LastModifiedByUserName = x.LastModifiedByUser != null ? x.LastModifiedByUser.Name : null,
                    CommentCount = x.CommentsCreatedBy.Count,
                    CreatedNewsCount = x.CreatedNews.Count,
                    CreatedNewsNames = x.CreatedNews.Select(n => n.Title).ToList(),
                    UpdatedNewsNames = x.UpdatedNews.Select(n => n.Title).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new AuFrameWorkException("Kullanıcı bulunamadı", "USER_NOT_FOUND", "NotFound");

            return user;
        }
    }
} 