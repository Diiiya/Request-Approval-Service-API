using System;
using System.Collections.Generic;

namespace RequestApprovalService.Api.UserPolicies.GetByPolicyId
{
    public class UserPoliciesGetByPolicyIdQueryResult
    {
        //public string Name { get; set; }
        //public IEnumerable<Domain.UserPolicies> UserPolicies { get; set; }
        //public Domain.User User { get; set; }
        //public Guid UserId { get; set; }
        public List<Guid> UserIds { get; set; }

        public static implicit operator List<object>(UserPoliciesGetByPolicyIdQueryResult v)
        {
            throw new NotImplementedException();
        }
    }
}
