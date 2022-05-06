using System;

namespace RequestApprovalService.Contracts
{
    public class PolicyGetAllResponse
    {
        public Guid PolicyId { get; set; }
        public string Name { get; set; }
    }
}
