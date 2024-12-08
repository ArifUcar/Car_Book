using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.CarResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers
{
    public class GetCarWithPricingQueryHandler
    {

        private readonly ICarPricingRepository _repository;
   

        public GetCarWithPricingQueryHandler(ICarPricingRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetCarWithPricingQueryResult>> Handle()
        {
            var values= await _repository.GetCarPricingWithCars();
            return values.Select(x=> new GetCarWithPricingQueryResult {
            
                CarID = x.CarID,
                PricingName = x.Pricing.Name,
                PricingAmount = x.Amount,
                Seat = x.Car.Seat,
                BigImageUrl = x.Car.BigImageUrl,
                CoverImageUrl = x.Car.CoverImageUrl,
                Fuel= x.Car.Fuel,
                Km=x.Car.Km,
                Luggage=x.Car.Luggage,
                Model=x.Car.Model,
                Transmisson=x.Car.Transmisson,
                BrandName=x.Car.Brand.Name,
            }).ToList();
        }
    }
}
