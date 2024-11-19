using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.AboutCommand;
using UdemyCarBook.Application.Features.CQRS.Commands.BannerCommand;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.BannerHandlers.WriteBannerHandlers
{
    public  class RemoveBannerCommandHandler
    {
        private readonly IRepository<Banner> _bannerrepository;

        public RemoveBannerCommandHandler(IRepository<Banner> bannerrepository)
        {
            _bannerrepository = bannerrepository;
        }
        public async Task Handle(RemoveBannerCommand command)
        {
            var value= await _bannerrepository.GetByIdAsync(command.Id);
             await _bannerrepository.RemoveAsync(value);
           
        }
    }
}
