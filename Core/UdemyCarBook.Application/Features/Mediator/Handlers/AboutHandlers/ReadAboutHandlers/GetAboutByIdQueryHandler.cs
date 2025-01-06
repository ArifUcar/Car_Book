using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AboutQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AboutResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AboutHandlers.ReadAboutHandlers
{
    public class GetAboutByIdQueryHandler:IRequestHandler<GetAboutByIdQuery,GetAboutByIdQueryResult>
    {
        private readonly IRepository<About> _repository;

        public GetAboutByIdQueryHandler(IRepository<About> repository)
        {
            _repository = repository;
        }

        public async Task<GetAboutByIdQueryResult> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetAboutByIdQueryResult
            {
                Title = values.Title,
                Description = values.Description,
                Id = request.Id,
                ImageUrl = values.ImageUrl,

            };
        }
    }
}
