using MediatR;
using System;

namespace UdemyCarBook.Application.Features.Mediator.Commands.UserCommands
{
    public class RemoveUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
} 