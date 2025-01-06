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
        /// <summary>
        /// Rol adı (Örn: Admin, Editör)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role atanmış kullanıcılar
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Rolü oluşturan kullanıcı
        /// </summary>
        [ForeignKey("CreatedById")]
        public virtual User CreatedByUser { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı
        /// </summary>
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedByUser { get; set; }
    }
}
