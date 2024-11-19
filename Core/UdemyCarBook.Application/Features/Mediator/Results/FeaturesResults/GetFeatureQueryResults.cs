using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Results
{
    public class GetFeatureQueryResults
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }

    }
}
