using MediatR;
using System;
using UdemyCarBook.Application.Features.Mediator.Results.NewsResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.NewsQueries
{
    public class GetNewsByIdQuery : IRequest<GetNewsByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
} 