using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UdemyCarBook.Application.Locations.Mediator.Commands.LocationsCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Locations.Mediator.Handlers.LocationHandlers.WriteLocationHandlers
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
    {
        private readonly IRepository<Location> _repository;

        public UpdateLocationCommandHandler(IRepository<Location> repository)
        {
            this._repository = repository;
        }

        public async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByIdAsync(request.LocationID);
          
            values.LocationID = request.LocationID;
            values.Name = request.Name;
            await _repository.UpdateAsync(values);
            
        }
    }
}
