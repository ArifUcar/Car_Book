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

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ServiceHandlers.ReadFeatureHandlers
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdQueryResults>
    {
        private readonly IRepository<Service> _repository;

        public GetServiceByIdQueryHandler(IRepository<Service> repository)
        {
            _repository = repository;
        }

        public async Task<GetServiceByIdQueryResults> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
    
                var values = await _repository.GetByIdAsync(request.Id);
                return new GetServiceByIdQueryResults
                {
                    ServiceID = values.ServiceID,
                    Title = values.Title,
                    Description = values.Description,
                    IconUrl = values.IconUrl,

                };
            
        }
    }
}
