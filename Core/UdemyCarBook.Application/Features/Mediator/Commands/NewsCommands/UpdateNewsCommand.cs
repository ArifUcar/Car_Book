using MediatR;
using System;
using System.Collections.Generic;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Commands.NewsCommands
{
    public class UpdateNewsCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AuthorId { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public NewsStatus Status { get; set; }
        public List<Guid> TagIds { get; set; }
    }
}