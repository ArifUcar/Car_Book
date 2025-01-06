using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Results.AuthorResults
{
    public class GetAuthorByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<AuthorNewsDto> News { get; set; }
        public List<AuthorSocialMediaDto> SocialMediaAccounts { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByUserName { get; set; }
    }

    public class AuthorNewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string CategoryName { get; set; }
    }

    public class AuthorSocialMediaDto
    {
        public Guid Id { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
    }
} 