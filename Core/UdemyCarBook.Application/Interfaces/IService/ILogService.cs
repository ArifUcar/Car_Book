using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces.IService
{
    public interface ILogService
    {
       
            Task CreateLog(string title, string description, string processType, string processLocation);
            Task CreateErrorLog(System.Exception ex, string location, string additionalInfo = null);
            Task<List<Log>> GetAllLogs();
            Task<List<Log>> GetErrorLogs();
            Task<List<Log>> GetLogsByDateRange(DateTime startDate, DateTime endDate);
            Task<List<Log>> GetLogsByProcessType(string processType);

    }
}
