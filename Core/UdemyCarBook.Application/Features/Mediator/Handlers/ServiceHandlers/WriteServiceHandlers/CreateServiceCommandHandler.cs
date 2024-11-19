using MediatR;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Services.Mediator.Commands.ServiceCommands;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Services.Mediator.Handlers.ServiceHandlers.WriteServiceHandlers
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand>
    {
        private readonly IRepository<Service> _repoistory;

        public CreateServiceCommandHandler(IRepository<Service> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            await _repoistory.CreateAsync(new Service {
            Title=request.Title,
            Description=request.Description,
            IconUrl=request.IconUrl,  
            });
        }
    }
}
