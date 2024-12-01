using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories.IncludeRepostiries
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private readonly CarBookContext _context;

        public CarBrandRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetCarsListWithBrandAsync()
        {
            var values = await _context.Cars.Include(x => x.Brand).ToListAsync();
            return values;
        }

        public async Task<List<Car>> GetLast5CarsListWithBrandAsync()
        {
            var values = await _context.Cars.Include(x=>x.Brand).OrderByDescending(x => x.CarID).Take(5).ToListAsync();
            return values;
        }
    }
}
