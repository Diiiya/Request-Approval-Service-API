using System;

namespace RequestApprovalService.Contracts
{
    public class RequestUpdateRequest
    {
        public Guid RequestId { get; set; }
        public Guid PolicyId { get; set; }
        public Guid UserId { get; set; }
        public Decimal Code { get; set; }
        public bool UserAnswer { get; set; }
    }
}
