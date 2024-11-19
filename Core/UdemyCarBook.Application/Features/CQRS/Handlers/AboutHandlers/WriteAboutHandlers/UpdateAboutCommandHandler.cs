using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.AboutCommand;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers.WriteAboutHandlers
{
    public class UpdateAboutCommandHandler
    {
        private readonly IRepository<About> _repository;

        public UpdateAboutCommandHandler(IRepository<About> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateAboutCommand updateAboutCommand)
        {
            var values = await _repository.GetByIdAsync(updateAboutCommand.AboutId);
            values.AboutId = updateAboutCommand.AboutId;
            values.Title = updateAboutCommand.Title;
            values.Description = updateAboutCommand.Description;
            values.ImageUrl = updateAboutCommand.ImageUrl;
            await _repository.UpdateAsync(values);
        }
    }
}
