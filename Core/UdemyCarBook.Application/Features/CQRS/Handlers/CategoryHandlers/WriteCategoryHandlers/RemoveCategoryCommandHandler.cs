using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.CategoryCommand;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CategoryHandlers.WriteCategoryHandlers
{
    public class RemoveCategoryCommandHandler
    {
        private readonly IRepository<Category > _categoryRepository;

        public RemoveCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Handle(RemoveCategoryCommand command)
        {
            var value = await _categoryRepository.GetByIdAsync(command.Id);
            await _categoryRepository.RemoveAsync(value);
        }
    }
}
