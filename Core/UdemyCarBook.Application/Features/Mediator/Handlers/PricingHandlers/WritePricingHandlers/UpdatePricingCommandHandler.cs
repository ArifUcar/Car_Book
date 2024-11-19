﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UdemyCarBook.Application.Pricings.Mediator.Commands.PricingsCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Pricings.Mediator.Handlers.PricingHandlers.WritePricingHandlers
{
    public class UpdatePricingCommandHandler : IRequestHandler<UpdatePricingCommand>
    {
        private readonly IRepository<Pricing> _repository;

        public UpdatePricingCommandHandler(IRepository<Pricing> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdatePricingCommand request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByIdAsync(request.PricingID);
          
            values.PricingID = request.PricingID;
            values.Name = request.Name;
            
            await _repository.UpdateAsync(values);
            
        }
    }
}
