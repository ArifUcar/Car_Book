using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.FooterAddressCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.FooterAddressQueries.WriteFooterAddressHandlers
{
    public class UpdateFooterAddressCommandHandler : IRequestHandler<UpdateFooterAddresssCommand>
    {
        private readonly IRepository<FooterAddress> _repository;

        public UpdateFooterAddressCommandHandler(IRepository<FooterAddress> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateFooterAddresssCommand request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByIdAsync(request.FooterAddressID);
            values.Phone = request.Phone;
            values.Description = request.Description;
            values.Address = request.Address;
            values.Email = request.Email;
            values.FooterAddressID = request.FooterAddressID;
            await _repository.UpdateAsync(values);
        }
    }
}
