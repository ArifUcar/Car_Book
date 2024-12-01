using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.BlogCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers.WriteBlogHandlers
{
    public class RemoveBlogCommandHandlers : IRequestHandler<RemoveBlogCommand>
    {
        private readonly IRepository<Blog> _blogRepository;

        public RemoveBlogCommandHandlers(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            var values= await _blogRepository.GetByIdAsync(request.Id);
           
                await _blogRepository.RemoveAsync(values);
       
         

        
        }
    }
}
