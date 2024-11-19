using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.ContactCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.ContactHandlers.WriteContactHandlers
{
    public class UpdateContactCommandHandler
    {
        private readonly IRepository<Contact> _contactRepository;

        public UpdateContactCommandHandler(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task Handle(UpdateContactCommand command) {
        
            var values= await _contactRepository.GetByIdAsync(command.ContactID);
            values.ContactID = command.ContactID;
            values.Subject = command.Subject;
            values.Email = command.Email;
            values.Message = command.Message;
            values.Name = command.Name;
            values.SendDate = command.SendDate;
            await _contactRepository.UpdateAsync(values);
        
        }

    }
}
