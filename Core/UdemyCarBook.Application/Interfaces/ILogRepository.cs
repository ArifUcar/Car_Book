using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Interfaces
{
    public interface ILogRepository
    {
        Task CreateLog(string title, string description, string processType, string processLocation);
    }
}
