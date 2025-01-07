using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyCarBook.Domain.Entities
{
    public class Role : BaseEntity
    {
    
        public string Name { get; set; }

  
        public virtual ICollection<User> Users { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUserId")]
        public virtual User UpdatedByUser { get; set; }

        [ForeignKey("LastModifiedByUserId")]
        public virtual User LastModifiedByUser { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
