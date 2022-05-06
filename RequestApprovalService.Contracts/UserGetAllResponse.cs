using System;

namespace RequestApprovalService.Contracts
{
    public class UserGetAllResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}
