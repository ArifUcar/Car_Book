using MediatR;
using System;

namespace UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands
{
    public class RemoveAuthorCommand : IRequest
    {
        public Guid Id { get; set; }
    }
} 