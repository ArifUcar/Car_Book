using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.FeatureQueries;
using UdemyCarBook.Application.Features.Mediator.Results;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.FeatureHandlers.ReadFeatureHandlers
{
    public class GetFeatureQueryHandler : IRequestHandler<GetFeatureQuery, List<GetFeatureQueryResults>>
    {
        private readonly IRepository<Feature> _repoistory;

        public GetFeatureQueryHandler(IRepository<Feature> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task<List<GetFeatureQueryResults>> Handle(GetFeatureQuery request, CancellationToken cancellationToken)
        {
            var values = await _repoistory.GetAllAsync();
            return values.Select(x => new GetFeatureQueryResults
            {
                FeatureID = x.FeatureID,
                Name = x.Name,
            }).ToList();
        }
    }
}
