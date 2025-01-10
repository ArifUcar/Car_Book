using MediatR;
using System;

namespace UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands
{
    public class SoftDeleteAuthorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid LastModifiedById { get; set; }
    }
} 