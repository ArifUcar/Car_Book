using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Locations.Mediator.Results
{
    public class GetLocationQueryResults
    {
        public int LocationID { get; set; }
        public string Name { get; set; }

    }
}
