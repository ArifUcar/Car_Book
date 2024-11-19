using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.ContactResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.ContactHandlers.ReadContactHandlers
{
    public class GetContactQueryHandler
    {
        private readonly IRepository<Contact> _contactRepository;

        public GetContactQueryHandler(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<List<GetContactQueryResult>> Handle()
        {
            var values=  await _contactRepository.GetAllAsync();
            return values.Select( x=> new GetContactQueryResult { ContactID=x.ContactID , Name=x.Name, Email=x.Email, Message=x.Message , SendDate=x.SendDate, Subject=x.Subject }).ToList();
         
        }
    }
}
