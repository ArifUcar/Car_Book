﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.CQRS.Commands.CategoryCommand
{
    public class UpdateCategoryCommand
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
