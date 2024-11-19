using MediatR;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Pricings.Mediator.Commands.PricingsCommands;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Pricings.Mediator.Handlers.PricingHandlers.WritePricingHandlers
{
    public class CreatePricingCommandHandler : IRequestHandler<CreatePricingCommand>
    {
        private readonly IRepository<Pricing> _repoistory;

        public CreatePricingCommandHandler(IRepository<Pricing> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task Handle(CreatePricingCommand request, CancellationToken cancellationToken)
        {
            await _repoistory.CreateAsync(new Pricing {
            Name=request.Name,
            });
        }
    }
}
