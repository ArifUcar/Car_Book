using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class Tag : BaseEntity
    {
        /// <summary>
        /// Etiket adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Etiketin bağlı olduğu haberler
        /// </summary>
        public virtual ICollection<News> News { get; set; }
    }
}
