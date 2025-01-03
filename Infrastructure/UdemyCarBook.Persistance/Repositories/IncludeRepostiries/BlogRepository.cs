﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories.IncludeRepostiries
{
  public  class BlogRepository : IBlogRepository
    {
        private readonly CarBookContext _context;

        public BlogRepository(CarBookContext context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetLast3BlogsListWithAuthorAsync()
        {
            var values= await _context.Blogs.Include(x=>x.Author).OrderByDescending(x=> x.AuthorId).Take(3).ToListAsync();
            return values;
        }

        public async Task<List<Blog>> GetAllBlogsListWithAuthorAsync()
        {
            var values = await _context.Blogs.Include(x => x.Author).Include(y => y.Category).ToListAsync();
            return values;
        }
    }
}
