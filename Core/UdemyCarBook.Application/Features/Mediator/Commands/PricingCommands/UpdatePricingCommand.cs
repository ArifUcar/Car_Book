using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Pricings.Mediator.Commands.PricingsCommands
{
    public class UpdatePricingCommand:IRequest
    {
        public int PricingID { get; set; }
        public string Name { get; set; }

    }
}
