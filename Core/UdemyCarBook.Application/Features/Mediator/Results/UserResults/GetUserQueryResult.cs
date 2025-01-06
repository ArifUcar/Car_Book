using System;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Results.UserResults
{
    public class GetUserQueryResult
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public List<string> RoleNames { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public int CommentCount { get; set; }
        public int CreatedNewsCount { get; set; }
    }
} 