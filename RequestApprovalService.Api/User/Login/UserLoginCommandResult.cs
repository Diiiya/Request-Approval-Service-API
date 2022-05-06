using System;

namespace RequestApprovalService.Api.User.Login
{
    public class UserLoginCommandResult
    {
        public bool IsSuccess { get; internal set; }
        public Guid UserId { get; set; }
        public bool isAdmin { get; set; }
    }
}
