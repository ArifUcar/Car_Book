using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.UserQueries;
using UdemyCarBook.Application.Features.Mediator.Results.UserResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.UserHandlers.ReadUserHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<GetUserQueryResult>>
    {
        private readonly IRepository<User> _repository;

        public GetUserQueryHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetUserQueryResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAll()
                .Include(x => x.Roles)
                .Include(x => x.CreatedByUser)
                .Include(x => x.CommentsCreatedBy)
                .Include(x => x.CreatedNews)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new GetUserQueryResult
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    UserType = x.UserType,
                    RoleNames = x.Roles.Select(r => r.Name).ToList(),
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser.Name,
                    CommentCount = x.CommentsCreatedBy.Count,
                    CreatedNewsCount = x.CreatedNews.Count
                })
                .ToListAsync(cancellationToken);

            return users;
        }
    }
} 