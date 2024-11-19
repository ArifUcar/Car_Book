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
    public class RemoveCarCommandHandler
    {
        private readonly IRepository<Car> _carRepository;

        public RemoveCarCommandHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task Handle(RemoveCarCommand command) 
        {
            var value= await _carRepository.GetByIdAsync(command.Id);
            await _carRepository.RemoveAsync(value);

        }
    }
}
