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
    public class GetAboutQueryHandler:IRequestHandler<GetAboutQuery,List<GetAboutQueryResult>>
    {
        private readonly IRepository<About> _repository;

        public GetAboutQueryHandler(IRepository<About> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAboutQueryResult>> Handle(GetAboutQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Where(x => !x.IsDeleted)
                         .Select(x => new GetAboutQueryResult
                         {
                             Id = x.Id,
                             Title = x.Title,
                             Description = x.Description,
                             ImageUrl = x.ImageUrl,
                         }).ToList();
        }
    }
}
