using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.UserCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Domain.Exceptions;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.UserHandlers.WriteUserHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IHistoryService _historyService;
        private readonly ILogService _logService;

        public UpdateUserCommandHandler(IRepository<User> repository, IHistoryService historyService, ILogService logService)
        {
            _repository = repository;
            _historyService = historyService;
            _logService = logService;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.GetByIdAsync(request.Id);
                if (user == null)
                    throw new AuFrameWorkException("Kullanıcı bulunamadı", "USER_NOT_FOUND", "NotFound");

                if (string.IsNullOrEmpty(request.UserName))
                    throw new AuFrameWorkException("Kullanıcı adı boş olamaz", "Name_REQUIRED", "ValidationError");

                if (string.IsNullOrEmpty(request.Email))
                    throw new AuFrameWorkException("E-posta adresi boş olamaz", "EMAIL_REQUIRED", "ValidationError");

                var existingUser = await _repository.GetFirstOrDefaultAsync(x => 
                    x.Id != request.Id && 
                    (x.UserName == request.UserName || x.Email == request.Email)
                );
                if (existingUser != null)
                    throw new AuFrameWorkException("Bu kullanıcı adı veya e-posta adresi zaten kullanılıyor", "USER_EXISTS", "ValidationError");

                user.UserName = request.UserName;
                if (!string.IsNullOrEmpty(request.Password))
                    user.Password = request.Password; 
                user.Email = request.Email;
                user.UserType = request.UserType;
                user.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(user);
                await _historyService.SaveHistory(user, "Update");
                
                await _logService.CreateLog(
                    "Kullanıcı Güncelleme",
                    $"'{request.UserName}' kullanıcı adlı kullanıcı güncellendi",
                    "Update",
                    "User"
                );
            }
            catch (Exception ex) when (ex is not AuFrameWorkException)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "UserUpdate",
                    $"Kullanıcı güncellenirken hata: {request.UserName}"
                );
                throw new AuFrameWorkException(
                    "Kullanıcı güncellenirken bir hata oluştu", 
                    "UPDATE_ERROR",
                    "Error"
                );
            }
        }
    }
} 