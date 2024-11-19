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
    public class UpdateBannerCommandHandler
    {
        private readonly IRepository<Banner> _bannerRepository;

        public UpdateBannerCommandHandler(IRepository<Banner> bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task Handle(UpdateBannerCommand command)
        {
            var values = await _bannerRepository.GetByIdAsync(command.BannerId);
            values.BannerId = command.BannerId;
            values.Title = command.Title;
            values.VideoDescription = command.VideoDescription;
            values.VideoURL = command.VideoURL;
            values.Description = command.Description;
            await _bannerRepository.UpdateAsync(values);

        }
    }
}
