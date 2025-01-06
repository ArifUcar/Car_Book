using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Results.UserResults
{
    public class GetUserByIdQueryResult
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public List<UserNewsDto> CreatedNews { get; set; }
        public List<UserCommentDto> CreatedComments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByUserName { get; set; }
    }

    public class UserNewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string CategoryName { get; set; }
    }

    public class UserCommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string NewsTitle { get; set; }
        public bool IsApproved { get; set; }
    }
} 