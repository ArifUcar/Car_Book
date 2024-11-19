﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Pricings.Mediator.Commands.PricingsCommands
{
    public class CreatePricingCommand:IRequest
    {
        public string Name { get; set; }

    }
}
