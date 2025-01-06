using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly NewsContext _context;

        public LogRepository(NewsContext context)
        {
            _context = context;
        }

        public async Task CreateLog(string title, string description, string processType, string processLocation)
        {
            var log = new Log
            {
                Title = title,
                Description = description,
                ProcessType = processType,
                ProcessLocation = processLocation,
                CreatedDate = DateTime.Now
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
