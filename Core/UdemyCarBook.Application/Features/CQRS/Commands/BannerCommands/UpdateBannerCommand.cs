using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.CQRS.Commands.BannerCommand
{
    public class UpdateBannerCommand
    {
        public int BannerId { get; set; }
        public int Title { get; set; }
        public int Description { get; set; }
        public int VideoDescription { get; set; }
        public int VideoURL { get; set; }

    }
}
