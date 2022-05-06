using System;

namespace RequestApprovalService.Contracts
{
    public class RequestGetAllResponse
    {
        public Guid RequestId { get; set; }
        public string Name { get; set; }
        public bool Approved { get; set; }
    }
}
