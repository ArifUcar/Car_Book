﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.CQRS.Commands.AboutCommand
{
    public class RemoveAboutCommand
    {
        public RemoveAboutCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
