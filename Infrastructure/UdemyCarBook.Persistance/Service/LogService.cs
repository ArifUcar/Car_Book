using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Service

    {
        public class LogService : ILogService
        {
            private readonly NewsContext _context;

            public LogService(NewsContext context)
            {
                _context = context;
            }

        public async Task CreateLog(string title, string description, string processType, string processLocation)
        {
            try
            {
                var log = new Log
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Description = description,
                    ProcessType = processType,
                    ProcessLocation = processLocation,
                    CreatedDate = DateTime.Now
                };

                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                // Log kaydetme hatası durumunda console'a yazalım
                Console.WriteLine($"Log kaydedilirken hata oluştu: {ex.Message}");
                throw;
            }
        }

        public async Task CreateErrorLog(System.Exception ex, string location, string additionalInfo = null)
        {
            try
            {
                var errorDetail = new
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message,
                    AdditionalInfo = additionalInfo,
                    Location = location,
                    Timestamp = DateTime.Now
                };

                var log = new Log
                {
                    Id = Guid.NewGuid(),
                    Title = "Sistem Hatası",
                    Description = System.Text.Json.JsonSerializer.Serialize(errorDetail),
                    ProcessType = "Error",
                    ProcessLocation = location,
                    CreatedDate = DateTime.Now
                };

                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception logEx)
            {
                // Hata logu kaydedilirken hata oluşursa console'a yazalım
                Console.WriteLine($"Hata logu kaydedilirken hata oluştu: {logEx.Message}");
                throw;
            }
        }

        public async Task<List<Log>> GetAllLogs()
        {
            return await _context.Logs
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Log>> GetErrorLogs()
        {
            return await _context.Logs
                .Where(x => x.ProcessType == "Error")
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Log>> GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Logs
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Log>> GetLogsByProcessType(string processType)
        {
            return await _context.Logs
                .Where(x => x.ProcessType == processType)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}
