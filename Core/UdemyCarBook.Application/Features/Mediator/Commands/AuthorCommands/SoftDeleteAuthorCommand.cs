using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands
{
    public class SoftDeleteAuthorCommand : IRequest
    {
        public Guid Id { get; set; }
    }
} 