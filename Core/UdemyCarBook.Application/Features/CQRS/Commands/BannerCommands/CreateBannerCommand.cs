using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.CQRS.Commands.BannerCommand
{
    public class CreateBannerCommand
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoDescription { get; set; }
        public string VideoURL { get; set; }

    }
}
