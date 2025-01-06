using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class SocialMedia : BaseEntity
    {
        /// <summary>
        /// Platform adı (Instagram, Facebook, Twitter vb.)
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Sosyal medya URL'i
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Platform ikonu (Font Awesome veya başka ikon kütüphanesi için)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Sıralama için kullanılacak değer
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Aktif/Pasif durumu
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Sosyal medya hesabının takipçi sayısı
        /// </summary>
        public int? FollowerCount { get; set; }

        /// <summary>
        /// Sosyal medya hesap adı/kullanıcı adı
        /// </summary>
        public string? AccountName { get; set; }

        /// <summary>
        /// İlişkili olduğu yazar (opsiyonel)
        /// </summary>
        public Guid? AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}
