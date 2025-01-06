using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyCarBook.Domain.Base
{
    /// <summary>
    /// Tüm entity'ler için temel sınıf
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Birincil anahtar
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Oluşturulma zamanı
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Son değişiklik zamanı
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Son değiştiren kullanıcı
        /// </summary>
        public Guid? UpdatedById { get; set; }

        /// <summary>
        /// Onaylı kayıt mı?
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Silindi mi?
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
