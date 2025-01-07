using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        // Navigation Properties
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<SocialMedia> SocialMediaAccounts { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUserId")]
        public virtual User UpdatedByUser { get; set; }

        [ForeignKey("LastModifiedByUserId")]
        public virtual User LastModifiedByUser { get; set; }

        public Author()
        {
            News = new HashSet<News>();
            SocialMediaAccounts = new HashSet<SocialMedia>();
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
