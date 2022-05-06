using System;

namespace RequestApprovalService.Contracts
{
    public class UserLoginResponse
    {
        public bool IsSuccess { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
