using System;

namespace UdemyCarBook.Application.Features.Mediator.Results.AuthorResults
{
    public class GetAuthorQueryResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int NewsCount { get; set; }
        public int SocialMediaCount { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? LastModifiedByUserName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
    }
} 