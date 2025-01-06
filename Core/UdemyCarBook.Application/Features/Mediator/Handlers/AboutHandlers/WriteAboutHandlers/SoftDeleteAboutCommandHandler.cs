using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AboutCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AboutHandlers.WriteAboutHandlers
{
    public class SoftDeleteAboutCommandHandler : IRequestHandler<SoftDeleteAboutCommand>
    {
        private readonly IRepository<About> _repository;
        private readonly ILogRepository _logRepository;

        public SoftDeleteAboutCommandHandler(IRepository<About> repository, ILogRepository logRepository)
        {
            _repository = repository;
       
            _logRepository = logRepository;
        }

        public async Task Handle(SoftDeleteAboutCommand request, CancellationToken cancellationToken)
            {
                await _repository.SoftDeleteAsync(request.Id);

                await _logRepository.CreateLog(
                    "About Silme",
                    $"'{request.Id}' id'li about silindi",
                    "Delete",
                    "About"
                );
            }
    }
}
