using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Şifre
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Kullanıcı tipi (Örn: Admin, Editör, Okuyucu)
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// Kullanıcının oluşturduğu kayıtlar
        /// </summary>
        public virtual ICollection<BaseEntity> CreatedRecords { get; set; }

        /// <summary>
        /// Kullanıcının güncellediği kayıtlar
        /// </summary>
        public virtual ICollection<BaseEntity> UpdatedRecords { get; set; }

        /// <summary>
        /// Kullanıcının yaptığı yorumlar
        /// </summary>
        public virtual ICollection<Comment> CommentsCreatedBy { get; set; }

        /// <summary>
        /// Kullanıcının sahip olduğu roller
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Kullanıcının oluşturduğu haberler
        /// </summary>
        public virtual ICollection<News> CreatedNews { get; set; }

        /// <summary>
        /// Kullanıcının güncellediği haberler
        /// </summary>
        public virtual ICollection<News> UpdatedNews { get; set; }

        /// <summary>
        /// Kullanıcının oluşturduğu roller
        /// </summary>
        public virtual ICollection<Role> CreatedRoles { get; set; }

        /// <summary>
        /// Kullanıcının güncellediği roller
        /// </summary>
        public virtual ICollection<Role> UpdatedRoles { get; set; }
        
    }
}
