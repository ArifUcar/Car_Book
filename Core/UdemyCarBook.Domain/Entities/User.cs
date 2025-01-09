using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UdemyCarBook.Domain.Base;
using UdemyCarBook.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyCarBook.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> CreatedRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> UpdatedRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> LastModifiedRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> CreatedByUsers { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> UpdatedByUsers { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> LastModifiedByUsers { get; set; }

        [JsonIgnore]
        public virtual User? CreatedByUser { get; set; }

        [JsonIgnore]
        public virtual User? UpdatedByUser { get; set; }

        [JsonIgnore]
        public virtual User? LastModifiedByUser { get; set; }

        [JsonIgnore]
        public virtual ICollection<News> CreatedNews { get; set; }

        [JsonIgnore]
        public virtual ICollection<News> UpdatedNews { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> CreatedComments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> UpdatedComments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Author> CreatedAuthors { get; set; }

        [JsonIgnore]
        public virtual ICollection<Author> UpdatedAuthors { get; set; }

        [JsonIgnore]
        public virtual ICollection<Category> CreatedCategories { get; set; }

        [JsonIgnore]
        public virtual ICollection<Category> UpdatedCategories { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tag> CreatedTags { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tag> UpdatedTags { get; set; }

        [JsonIgnore]
        public virtual ICollection<SocialMedia> CreatedSocialMedias { get; set; }

        [JsonIgnore]
        public virtual ICollection<SocialMedia> UpdatedSocialMedias { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> CreatedContacts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> UpdatedContacts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Newsletter> CreatedNewsletters { get; set; }

        [JsonIgnore]
        public virtual ICollection<Newsletter> UpdatedNewsletters { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<BaseEntity> CreatedRecords { get; set; }

        [NotMapped]
        [JsonIgnore]
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
