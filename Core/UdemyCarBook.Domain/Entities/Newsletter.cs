using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class Newsletter : BaseEntity
    {
        /// <summary>
        /// Abone email adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Abonelik durumu
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Abonelik tarihi
        /// </summary>
        public DateTime SubscriptionDate { get; set; }

        /// <summary>
        /// Abonelik iptal tarihi
        /// </summary>
        public DateTime? UnsubscribeDate { get; set; }
    }
}
