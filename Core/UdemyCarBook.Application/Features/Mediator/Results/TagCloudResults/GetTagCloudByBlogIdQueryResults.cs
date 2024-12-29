using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediator.Results.TagCloudResults
{
    public class GetTagCloudByBlogIdQueryResults
    {
        public int TagCloudID { get; set; }
        public string TagCloudTitle { get; set; }
        public int BlogId { get; set; }
    }
}
