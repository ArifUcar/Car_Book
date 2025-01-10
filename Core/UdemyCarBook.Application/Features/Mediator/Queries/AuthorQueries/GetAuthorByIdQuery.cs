using MediatR;
using System;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries
{
    public class GetAuthorByIdQuery : IRequest<GetAuthorByIdQueryResult>
    {
        public GetAuthorByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
} 