using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.AboutCommand;
using UdemyCarBook.Application.Features.CQRS.Commands.BrandCommand;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.BrandHandlers.WriteBannerHandlers
{
    public class RemoveBrandCommandHandler
    {
        private readonly IRepository<Brand> _brandRepositiory;

        public RemoveBrandCommandHandler(IRepository<Brand> brandRepositiory)
        {
            _brandRepositiory = brandRepositiory;
        }
        public async Task Handle(RemoveBrandCommand command)
        {
            var value= await _brandRepositiory.GetByIdAsync(command.Id);
            await _brandRepositiory.RemoveAsync(value);
        }
    }
}
