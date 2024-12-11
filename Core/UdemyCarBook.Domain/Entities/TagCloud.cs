using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Domain.Entities
{
    public class TagCloud
    {
        public int TagCloudID { get; set; }
        public string TagCloudTitle { get; set; }
        public int BlodId { get; set; }
        public Blog Blog { get; set; }
    }
}
