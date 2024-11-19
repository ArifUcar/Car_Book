using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.BrandResults;
using UdemyCarBook.Application.Features.CQRS.Results.CarResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers
{
    public class GetCarQueryHandler
    {
        private readonly IRepository<Car> _carRepository;

        public GetCarQueryHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<List<GetCarQueryResult>> Handle()
        {
            var values= await _carRepository.GetAllAsync();
            return values.Select(x=> new GetCarQueryResult {
            CarID = x.CarID,
            BigImageUrl = x.BigImageUrl,
            Seat=x.Seat,
            BrandID = x.BrandID,
            CoverImageUrl = x.CoverImageUrl,
            Km=x.Km,
            Luggage=x.Luggage,
            Model=x.Model,  
            Transmisson=x.Transmisson,
            
            
            }).ToList();
        }
    }
}
