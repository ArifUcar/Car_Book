using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UdemyCarBook.Application.Services.Mediator.Commands.ServicesCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Services.Mediator.Handlers.ServiceHandlers.WriteServiceHandlers
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IRepository<Service> _repository;

        public UpdateServiceCommandHandler(IRepository<Service> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByIdAsync(request.ServiceID);
          
            values.ServiceID = request.ServiceID;
            values.Title=request.Title;
            values.Description=request.Description;
            
            await _repository.UpdateAsync(values);
            
        }
    }
}
