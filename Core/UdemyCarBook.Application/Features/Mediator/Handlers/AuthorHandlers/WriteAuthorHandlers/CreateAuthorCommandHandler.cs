using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.WriteAuthorHandlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
    {
        private readonly IRepository<Author> _repository;

        public CreateAuthorCommandHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async  Task Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Author
            {
               AuthorName = request.AuthorName,
               ImageUrl = request.ImageUrl,
               Description = request.Description,
               



            });

        }
    }
}
