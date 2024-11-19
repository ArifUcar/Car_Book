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
    public class CreateCarCommandHandler
    {
        private readonly IRepository<Car> _carRepository;

        public CreateCarCommandHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task Handle(CreateCarCommand command)
        {
            await _carRepository.CreateAsync(new Car() {
                BigImageUrl = command.BigImageUrl,
                Seat = command.Seat,
                BrandID = command.BrandID,
                CoverImageUrl = command.CoverImageUrl,
                Km = command.Km,
                Luggage = command.Luggage,
                Model = command.Model,
                Transmisson = command.Transmisson,
                Fuel = command.Fuel,



            });
        }
    } }
