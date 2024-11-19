using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.SocialMedias.Mediator.Commands.SocialMediasCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.SocialMedias.Mediator.Handlers.SocialMediaHandlers.WriteSocialMediaHandlers
{
    public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand>
    {
        private readonly IRepository<SocialMedia> _repoistory;

        public CreateSocialMediaCommandHandler(IRepository<SocialMedia> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            await _repoistory.CreateAsync(new SocialMedia {
            Name=request.Name,
            Url=request.Url,
            Icon=request.Icon,
            });
        }
    }
}
