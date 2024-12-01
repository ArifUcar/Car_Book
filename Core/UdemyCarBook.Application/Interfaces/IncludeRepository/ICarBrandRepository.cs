using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces.IncludeRepository
{
    public interface ICarBrandRepository
    {
        Task<List<Car>> GetCarsListWithBrandAsync();
        Task<List<Car>> GetLast5CarsListWithBrandAsync();
    }
}
