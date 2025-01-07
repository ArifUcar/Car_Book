using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UdemyCarBook.Domain.Base;

namespace UdemyCarBook.Domain.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AuthorId { get; set; }
        public NewsStatus Status { get; set; }
        public int ViewCount { get; set; }

        // Navigation Properties
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUserId")]
        public virtual User UpdatedByUser { get; set; }

        [ForeignKey("LastModifiedByUserId")]
        public virtual User LastModifiedByUser { get; set; }

        public News()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
            ViewCount = 0;
        }
    }

    public enum NewsStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2
    }
}
