using System;

namespace RequestApprovalService.Api.Policy.GetAll
{
    public class PolicyGetAllQueryResult
    {
        public Guid PolicyId { get; set; }
        public string Name { get; set; }
    }
}
