using MediatR;
using System;

namespace UdemyCarBook.Application.Features.Mediator.Commands.UserCommands
{
    public class SoftDeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
} 