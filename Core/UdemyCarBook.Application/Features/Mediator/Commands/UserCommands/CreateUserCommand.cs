using MediatR;
using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Commands.UserCommands
{
    public class CreateUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
} 