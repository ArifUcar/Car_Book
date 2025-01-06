using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.WriteAuthorHandlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public CreateAuthorCommandHandler(IAuthorRepository repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _repository.IsEmailExistsAsync(request.Email))
                    throw new AuFrameWorkException("Bu e-posta adresi zaten kullanılıyor", "EMAIL_EXISTS", "ValidationError");

                var author = new Author
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    ImageUrl = request.ImageUrl,
                    Description = request.Description,
                    Email = request.Email,
                    CreatedDate = DateTime.UtcNow
                };

                await _repository.CreateAsync(author);
                await _historyService.SaveHistory(author, "Create");
                
                await _logService.CreateLog(
                    "Yazar Oluşturma",
                    $"'{author.Name} {author.Surname}' adlı yazar oluşturuldu",
                    "Create",
                    "Author"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "AuthorCreate",
                    $"Yazar oluşturulurken hata: {request.Email}"
                );
                throw new AuFrameWorkException(
                    "Yazar oluşturulurken bir hata oluştu", 
                    "CREATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 