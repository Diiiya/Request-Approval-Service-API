using System;
using System.Collections.Generic;

namespace RequestApprovalService.Contracts
{
    public class PolicyCreateRequest
    {
        public string Name { get; set; }
        public int Threshold { get; set; }
        //public int Secret { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
