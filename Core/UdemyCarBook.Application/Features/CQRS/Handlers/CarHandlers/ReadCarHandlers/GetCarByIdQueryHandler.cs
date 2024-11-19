using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Queries.CarQueries;
using UdemyCarBook.Application.Features.CQRS.Results.AboutResults;
using UdemyCarBook.Application.Features.CQRS.Results.CarResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers
{
    public class GetCarByIdQueryHandler
    {
        private readonly IRepository<Car> _carRepository;

        public GetCarByIdQueryHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
        {
            var values= await _carRepository.GetByIdAsync(query.Id);
            return new GetCarByIdQueryResult
            {
                BrandID=values.BrandID,
                CarID=values.CarID,
                CoverImageUrl=values.CoverImageUrl, 
                BigImageUrl=values.BigImageUrl, 
                Km=values.Km,
                Luggage=values.Luggage,
                Seat=values.Seat,
                Model=values.Model,
                Transmisson=values.Transmisson,
                
            };
        }
    }
}
