﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.BlogDtos
{
    public class ResultBlogWithAuthorDtos
    {

        public int BlogId { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }

        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}