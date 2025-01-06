using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UdemyCarBook.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyCarBook.Domain.Entities
{
    public class News : BaseEntity
    {
        /// <summary>
        /// Haber başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Haber içeriği
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Yayınlanma tarihi
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Kategori kimliği
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Kategori detayı
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Yazar kimliği
        /// </summary>
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }

        /// <summary>
        /// Haberi oluşturan kullanıcı
        /// </summary>


        /// <summary>
        /// Son güncelleyen kullanıcı
        /// </summary>
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedByUser { get; set; }

        /// <summary>
        /// Habere yapılan yorumlar
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Haber görseli URL'i
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Haber özeti
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Görüntülenme sayısı
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// Haberin durumu (Taslak, Yayında, Arşivde vb.)
        /// </summary>
        public NewsStatus Status { get; set; }

        /// <summary>
        /// Haberin etiketleri
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı ID'si
        /// </summary>
        [ForeignKey("LastModifiedByUserId")]
        public Guid? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı
        /// </summary>
 

    }

    public enum NewsStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2
    }
}
