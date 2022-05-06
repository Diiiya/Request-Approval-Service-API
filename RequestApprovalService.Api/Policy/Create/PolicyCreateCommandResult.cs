using System;

namespace RequestApprovalService.Api.Policy.Create
{
    public class PolicyCreateCommandResult
    {
        public Guid Id { get; set; }
        public int Secret { get; set; }
        public int Threshold { get; set; }
    }
}
