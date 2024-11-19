using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.CarResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers
{
    public class GetCarWithBrandQueryHandler
    {
        private readonly ICarBrandRepository _repository;

        public GetCarWithBrandQueryHandler(ICarBrandRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetCarWithBrandQueryResult>> Handle()

        {
            var values=await _repository.GetCarsListWithBrandAsync();
            return values.Select(x=>new GetCarWithBrandQueryResult
            {
            BrandName = x.Brand.Name,
            CarID = x.CarID,
            BigImageUrl = x.BigImageUrl,
            Seat = x.Seat,
            BrandID = x.BrandID,
            CoverImageUrl = x.CoverImageUrl,
            Km = x.Km,
            Luggage = x.Luggage,
            Model = x.Model,  
            Transmisson = x.Transmisson,
            
            
            }).ToList();
    }
    }
}
