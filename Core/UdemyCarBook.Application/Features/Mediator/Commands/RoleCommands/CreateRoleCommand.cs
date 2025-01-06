using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.RoleCommands
{
    public class CreateRoleCommand : IRequest
    {
        public string Name { get; set; }
    }
} 