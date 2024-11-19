using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.CarCommand;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.WriteCarHandlers
{
    public class UpdateCarCommandHandler
    {
        private readonly IRepository<Car> _carRepository;

        public UpdateCarCommandHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task Handle(UpdateCarCommand command)
        {
           var values= await _carRepository.GetByIdAsync(command.CarID);
            values.CarID = command.CarID;
            values.BrandID = command.BrandID;
            values.BigImageUrl = command.BigImageUrl;
            values.CoverImageUrl = command.CoverImageUrl;
            values.Model= command.Model;
            values.Luggage= command.Luggage;
            values.Seat=command.Seat;
            values.Transmisson=command.Transmisson;
            values.Km=command.Km;
            values.Fuel=command.Fuel;
            await _carRepository.UpdateAsync(values);

        }
    }
}
