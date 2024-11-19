using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Services.Mediator.Queries.ServiceQueries;
using UdemyCarBook.Application.Services.Mediator.Results;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Services.Mediator.Handlers.ServiceHandlers.ReadFeatureHandlers
{
    public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, List<GetServiceQueryResults>>
    {
        private readonly IRepository<Service> _repoistory;

        public GetServiceQueryHandler(IRepository<Service> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task<List<GetServiceQueryResults>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            var values = await _repoistory.GetAllAsync();
            return values.Select(x => new GetServiceQueryResults
            {
                ServiceID = x.ServiceID,
                Title = x.Title,
                Description = x.Description,
                IconUrl = x.IconUrl,
            }).ToList();
        }
    }
}
