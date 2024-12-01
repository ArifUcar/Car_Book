using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.CarResults;
using UdemyCarBook.Application.Interfaces.IncludeRepository;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers
{
    public class GetLast5CarWithBrandQueryHandler
    {
        private readonly ICarBrandRepository _repository;

        public GetLast5CarWithBrandQueryHandler(ICarBrandRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetLast5CarWithBrandQueryResult>> Handle()
        {
            var values = await _repository.GetLast5CarsListWithBrandAsync();
            return values.Select(x => new GetLast5CarWithBrandQueryResult
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

