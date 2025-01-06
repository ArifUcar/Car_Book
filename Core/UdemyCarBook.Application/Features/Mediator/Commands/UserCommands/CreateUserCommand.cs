using MediatR;
using System.Collections.Generic;
using UdemyCarBook.Domain.Enums;

namespace UdemyCarBook.Application.Features.Mediator.Commands.UserCommands
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
} 