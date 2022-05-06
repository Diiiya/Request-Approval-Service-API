using System;

namespace RequestApprovalService.Api.User.GetAll
{
    public class UserGetAllQueryResult
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
