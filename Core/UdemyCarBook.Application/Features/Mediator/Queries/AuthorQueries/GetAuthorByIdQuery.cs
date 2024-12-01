using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Results.AuthorsResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries
{
    public class GetAuthorByIdQuery:IRequest<GetAuthorByIdQueryResults>
    {
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
