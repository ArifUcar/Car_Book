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
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IRepository<Author> _repository;

        public UpdateAuthorCommandHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {

            var values = await _repository.GetByIdAsync(request.AuthorId);

            values.AuthorId = request.AuthorId;
            values.AuthorName = request.AuthorName;
            values.Description = request.Description;
            values.ImageUrl = request.ImageUrl;
            await _repository.UpdateAsync(values);

        }
    }
}
