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
    public class UpdateAboutCommandHandler :IRequestHandler<UpdateAboutCommand>
    {
        private readonly IRepository<About> _repository;
        private readonly ILogRepository _logService;

        public UpdateAboutCommandHandler(IRepository<About> repository, ILogRepository logService)
        {
            _repository = repository;
            _logService = logService;
        }
        public async Task Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            values.Description = request.Description;
            values.ImageUrl = request.ImageUrl;
            values.Title = request.Title;
            
            await _repository.UpdateAsync(values);

            await _logService.CreateLog(
                "About Güncelleme",
                $"'{request.Title}' başlıklı about güncellendi",
                "Update",
                "About"
            );
        }
    }
}
