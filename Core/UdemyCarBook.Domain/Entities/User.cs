using System;
using System.Collections.Generic;
using UdemyCarBook.Domain.Base;
using UdemyCarBook.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyCarBook.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string RefreshToken { get; set; }



        public DateTime? RefreshTokenExpireDate { get; set; }

        public Guid? UpdatedByUserId { get; set; }
        public Guid? LastModifiedByUserId { get; set; }

        // Navigation Properties
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Role> CreatedRoles { get; set; }
        public virtual ICollection<Role> UpdatedRoles { get; set; }
        public virtual ICollection<Role> LastModifiedRoles { get; set; }
        public virtual ICollection<User> CreatedByUsers { get; set; }
        public virtual ICollection<User> UpdatedByUsers { get; set; }
        public virtual ICollection<User> LastModifiedByUsers { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ICollection<News> CreatedNews { get; set; }
        public virtual ICollection<News> UpdatedNews { get; set; }
        public virtual ICollection<Comment> CreatedComments { get; set; }
        public virtual ICollection<Comment> UpdatedComments { get; set; }
        public virtual ICollection<Author> CreatedAuthors { get; set; }
        public virtual ICollection<Author> UpdatedAuthors { get; set; }
        public virtual ICollection<Category> CreatedCategories { get; set; }
        public virtual ICollection<Category> UpdatedCategories { get; set; }
        public virtual ICollection<Tag> CreatedTags { get; set; }
        public virtual ICollection<Tag> UpdatedTags { get; set; }
        public virtual ICollection<SocialMedia> CreatedSocialMedias { get; set; }
        public virtual ICollection<SocialMedia> UpdatedSocialMedias { get; set; }
        public virtual ICollection<Contact> CreatedContacts { get; set; }
        public virtual ICollection<Contact> UpdatedContacts { get; set; }
        public virtual ICollection<Newsletter> CreatedNewsletters { get; set; }
        public virtual ICollection<Newsletter> UpdatedNewsletters { get; set; }

        [NotMapped]
        public virtual ICollection<BaseEntity> CreatedRecords { get; set; }
        [NotMapped]
        public virtual ICollection<BaseEntity> UpdatedRecords { get; set; }

        public User()
        {
            Roles = new HashSet<Role>();
            CreatedRoles = new HashSet<Role>();
            UpdatedRoles = new HashSet<Role>();
            LastModifiedRoles = new HashSet<Role>();
            CreatedByUsers = new HashSet<User>();
            UpdatedByUsers = new HashSet<User>();
            LastModifiedByUsers = new HashSet<User>();
            CreatedNews = new HashSet<News>();
            UpdatedNews = new HashSet<News>();
            CreatedComments = new HashSet<Comment>();
            UpdatedComments = new HashSet<Comment>();
            CreatedAuthors = new HashSet<Author>();
            UpdatedAuthors = new HashSet<Author>();
            CreatedCategories = new HashSet<Category>();
            UpdatedCategories = new HashSet<Category>();
            CreatedTags = new HashSet<Tag>();
            UpdatedTags = new HashSet<Tag>();
            CreatedSocialMedias = new HashSet<SocialMedia>();
            UpdatedSocialMedias = new HashSet<SocialMedia>();
            CreatedContacts = new HashSet<Contact>();
            UpdatedContacts = new HashSet<Contact>();
            CreatedNewsletters = new HashSet<Newsletter>();
            UpdatedNewsletters = new HashSet<Newsletter>();
            CreatedRecords = new HashSet<BaseEntity>();
            UpdatedRecords = new HashSet<BaseEntity>();
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
