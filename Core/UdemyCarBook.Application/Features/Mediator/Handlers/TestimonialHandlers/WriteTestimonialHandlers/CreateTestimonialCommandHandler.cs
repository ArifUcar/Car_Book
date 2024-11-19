using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Application.Features.Mediator.Commands.TestimonialCommands;

namespace UdemyCarBook.Application.Testimonials.Mediator.Handlers.TestimonialHandlers.WriteTestimonialHandlers
{
    public class CreateTestimonialCommandHandler : IRequestHandler<CreateTestimonialCommand>
    {
        private readonly IRepository<Testimonial> _repoistory;

        public CreateTestimonialCommandHandler(IRepository<Testimonial> repoistory)
        {
            _repoistory = repoistory;
        }

        public async Task Handle(CreateTestimonialCommand request, CancellationToken cancellationToken)
        {
            await _repoistory.CreateAsync(new Testimonial {
            Name=request.Name,
           Title=request.Title,
           Comment=request.Comment,
           ImageUrl=request.ImageUrl,
            });
        }
    }
}
