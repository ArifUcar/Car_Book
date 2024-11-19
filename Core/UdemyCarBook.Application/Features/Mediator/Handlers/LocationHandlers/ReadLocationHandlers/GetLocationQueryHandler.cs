using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Locations.Mediator.Queries.LocationQueries;
using UdemyCarBook.Application.Locations.Mediator.Results;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Locations.Mediator.Handlers.LocationHandlers.ReadFeatureHandlers
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, List<GetLocationQueryResults>>
    {
        private readonly IRepository<Location> _repoistory;

        public GetLocationQueryHandler(IRepository<Location> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task<List<GetLocationQueryResults>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var values = await _repoistory.GetAllAsync();
            return values.Select(x => new GetLocationQueryResults
            {
                LocationID = x.LocationID,
                Name = x.Name,
            }).ToList();
        }
    }
}
