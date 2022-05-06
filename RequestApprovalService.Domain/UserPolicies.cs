using System;

namespace RequestApprovalService.Domain
{
    public class UserPolicies
    {
        public Guid UserId { get; set; }
        public Guid PolicyId { get; set; }
        public User User { get; set; }
        public Policy Policy { get; set; }
    }
}
