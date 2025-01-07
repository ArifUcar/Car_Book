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
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public UpdateAuthorCommandHandler(IAuthorRepository repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _repository.GetByIdWithDetailsAsync(request.Id);
                if (author == null)
                    throw new AuFrameWorkException("Yazar bulunamadı", "AUTHOR_NOT_FOUND", "NotFound");

                if (author.Email != request.Email && await _repository.IsEmailExistsAsync(request.Email))
                    throw new AuFrameWorkException("Bu e-posta adresi zaten kullanılıyor", "EMAIL_EXISTS", "ValidationError");

                author.FirstName = request.Name;
                author.LastName = request.Surname;
                author.ImageUrl = request.ImageUrl;
                author.Description = request.Description;
                author.Email = request.Email;
                author.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(author);
                await _historyService.SaveHistory(author, "Update");
                
                await _logService.CreateLog(
                    "Yazar Güncelleme",
                    $"'{author.FirstName} {author.LastName}' adlı yazar güncellendi",
                    "Update",
                    "Author"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "AuthorUpdate",
                    $"Yazar güncellenirken hata: {request.Id}"
                );
                throw new AuFrameWorkException(
                    "Yazar güncellenirken bir hata oluştu", 
                    "UPDATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 