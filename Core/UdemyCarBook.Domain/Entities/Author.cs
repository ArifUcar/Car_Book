using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class Author : BaseEntity
    {
        /// <summary>
        /// Yazarın adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Yazarın soyadı
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Yazarın profil fotoğrafı
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Yazar hakkında kısa bilgi
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Yazarın mail adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Yazarın sosyal medya hesapları
        /// </summary>
        public virtual ICollection<SocialMedia> SocialMediaAccounts { get; set; }

        /// <summary>
        /// Yazarın yazdığı haberler
        /// </summary>
        public virtual ICollection<News> News { get; set; }
    }
}
