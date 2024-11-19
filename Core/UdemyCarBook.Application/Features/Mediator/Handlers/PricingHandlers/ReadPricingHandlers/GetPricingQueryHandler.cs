using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Pricings.Mediator.Queries.PricingQueries;
using UdemyCarBook.Application.Pricings.Mediator.Results;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Pricings.Mediator.Handlers.PricingHandlers.ReadFeatureHandlers
{
    public class GetPricingQueryHandler : IRequestHandler<GetPricingQuery, List<GetPricingQueryResults>>
    {
        private readonly IRepository<Pricing> _repoistory;

        public GetPricingQueryHandler(IRepository<Pricing> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task<List<GetPricingQueryResults>> Handle(GetPricingQuery request, CancellationToken cancellationToken)
        {
            var values = await _repoistory.GetAllAsync();
            return values.Select(x => new GetPricingQueryResults
            {
                PricingID = x.PricingID,
               Name = x.Name,
            }).ToList();
        }
    }
}
