using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.WriteAuthorHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
    {
        private readonly IRepository<Author> _repository;

        public UpdateAuthorCommandHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdAsync(request.Id);
            if (author == null)
                throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

            author.FirstName = request.FirstName;
            author.LastName = request.LastName;
            author.Description = request.Description;
            author.ImageUrl = request.ImageUrl;
            author.Email = request.Email;
            author.IsActive = request.IsActive;
            author.LastModifiedDate = DateTime.UtcNow;
            author.LastModifiedByUserId = request.LastModifiedById;

            await _repository.UpdateAsync(author);
            return Unit.Value;
        }
    }
} 