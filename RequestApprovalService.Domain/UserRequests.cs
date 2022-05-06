using System;

namespace RequestApprovalService.Domain
{
    public class UserRequests
    {
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
        public bool? UserAnswer { get; set; }
        public virtual User User { get; set; }
        public virtual Request Request { get; set; }
    }
}
