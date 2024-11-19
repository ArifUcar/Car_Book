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

namespace UdemyCarBook.Application.Features.Mediator.Handlers.LocationHandlers.ReadFeatureHandlers
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResults>
    {
        private readonly IRepository<Location> _repository;

        public GetLocationByIdQueryHandler(IRepository<Location> repository)
        {
            _repository = repository;
        }

        public async Task<GetLocationByIdQueryResults> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
    
                var values = await _repository.GetByIdAsync(request.Id);
                return new GetLocationByIdQueryResults
                {
                    LocationID = values.LocationID,
                    Name = values.Name,

                };
            
        }
    }
}
