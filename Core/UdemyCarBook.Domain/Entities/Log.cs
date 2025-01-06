using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProcessType { get; set; } // Create, Update, Delete, SoftDelete vb.
        public string ProcessLocation { get; set; } // Hangi entity üzerinde işlem yapıldı
    }
}
