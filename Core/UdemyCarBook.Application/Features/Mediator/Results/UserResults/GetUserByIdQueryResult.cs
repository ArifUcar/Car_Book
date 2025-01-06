using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Results.UserResults
{
    public class GetUserByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public List<string> RoleNames { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByUserName { get; set; }
        public int CommentCount { get; set; }
        public int CreatedNewsCount { get; set; }
        public List<string> CreatedNewsNames { get; set; }
        public List<string> UpdatedNewsNames { get; set; }
    }
} 