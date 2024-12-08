using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediator.Commands.BlogCommands
{
    public class UpdateBlogCommand:IRequest
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
