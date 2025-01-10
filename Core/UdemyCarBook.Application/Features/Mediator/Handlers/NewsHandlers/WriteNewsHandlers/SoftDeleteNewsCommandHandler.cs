using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.NewsCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.NewsHandlers.WriteNewsHandlers
{
    public class SoftDeleteNewsCommandHandler : IRequestHandler<SoftDeleteNewsCommand, Unit>
    {
        private readonly INewsRepository _repository;

        public SoftDeleteNewsCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _repository.GetByIdAsync(request.Id);
            if (news == null)
                throw new AuFrameWorkException("Haber bulunamadı", "NEWS_NOT_FOUND", "NotFound");

            news.IsDeleted = true;
            news.LastModifiedDate = DateTime.UtcNow;
            news.LastModifiedByUserId = request.LastModifiedById;

            await _repository.UpdateAsync(news);
            return Unit.Value;
        }
    }
} 