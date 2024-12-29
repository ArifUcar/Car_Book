using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediator.Commands.TagCloudCommands
{
    public class UpdateTagCloudCommand :IRequest
    {
        public int TagCloudID { get; set; }
        public string TagCloudTitle { get; set; }
        public int BlodId { get; set; }
    }
}
