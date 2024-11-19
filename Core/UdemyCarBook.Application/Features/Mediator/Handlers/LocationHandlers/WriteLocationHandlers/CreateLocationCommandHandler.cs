using MediatR;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Locations.Mediator.Commands.LocationCommands;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Locations.Mediator.Handlers.LocationHandlers.WriteLocationHandlers
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand>
    {
        private readonly IRepository<Location> _repoistory;

        public CreateLocationCommandHandler(IRepository<Location> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            await _repoistory.CreateAsync(new Location {
            Name=request.Name
            });
        }
    }
}
