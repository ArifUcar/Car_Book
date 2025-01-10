using MediatR;
using System;

namespace UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands
{
    public class CreateAuthorCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedById { get; set; }
    }
} 