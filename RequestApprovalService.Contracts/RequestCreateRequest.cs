using System;

namespace RequestApprovalService.Contracts
{
    public class RequestCreateRequest
    {
        public string Name { get; set; }
        public Guid PolicyId { get; set; }
    }
}
