using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.BlogCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers.WriteBlogHandlers
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
    {
        private readonly IRepository<Blog> _repository;

        public UpdateBlogCommandHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByIdAsync(request.BlogId);
           
               values.BlogId = request.BlogId;
                values.Title = request.Title;
            values.CategoryId= request.CategoryId;
            values.CategoryId = request.CategoryId;
            values.CreatedDate = request.CreatedDate;
                await _repository.UpdateAsync(values);
         
        }
    }
}
