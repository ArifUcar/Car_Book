using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyCarBook.Persistance.Repositories.IncludeRepostiries
{
    public class CarPricingRepository : ICarPricingRepository
    {
        private readonly CarBookContext _context;

        public CarPricingRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<List<CarPricing>> GetCarPricingWithCars()
        {
            var values = await _context.CarPricing
                .Include(x => x.Car)
                .ThenInclude(y => y.Brand)
                .Include(x => x.Pricing)
                
                .ToListAsync();

            return values;
        }
    }
}
