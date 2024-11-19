﻿using MediatR;
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
    public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand>
    {
        private readonly IRepository<SocialMedia> _repository;

        public UpdateSocialMediaCommandHandler(IRepository<SocialMedia> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByIdAsync(request.SocialMediaID);
          
            values.SocialMediaID = request.SocialMediaID;
            values.Name = request.Name;
            values.Url = request.Url;
            values.Icon = request.Icon;
            await _repository.UpdateAsync(values);
            
        }
    }
}