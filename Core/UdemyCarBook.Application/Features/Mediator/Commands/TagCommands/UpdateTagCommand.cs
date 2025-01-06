using MediatR;
using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Commands.TagCommands
{
    public class UpdateTagCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> NewsIds { get; set; }
    }
} 