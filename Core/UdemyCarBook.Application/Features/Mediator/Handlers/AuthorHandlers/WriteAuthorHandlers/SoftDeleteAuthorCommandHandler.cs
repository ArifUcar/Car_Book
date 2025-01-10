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
    public class SoftDeleteAuthorCommandHandler : IRequestHandler<SoftDeleteAuthorCommand, Unit>
    {
        private readonly IRepository<Author> _repository;

        public SoftDeleteAuthorCommandHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdAsync(request.Id);
            if (author == null)
                throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

            author.IsDeleted = true;
            author.LastModifiedDate = DateTime.UtcNow;
            author.LastModifiedByUserId = request.LastModifiedById;

            await _repository.UpdateAsync(author);
            return Unit.Value;
        }
    }
} 