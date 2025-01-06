using MediatR;
using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Commands.TagCommands
{
    public class CreateTagCommand : IRequest
    {
        public string Name { get; set; }
        public List<Guid> NewsIds { get; set; }
    }
} 