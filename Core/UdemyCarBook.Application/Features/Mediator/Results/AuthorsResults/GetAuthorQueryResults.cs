using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediator.Results.AuthorsResults
{
    public class GetAuthorQueryResults
    {
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string ImageUrl { get; set; }
    }
}
