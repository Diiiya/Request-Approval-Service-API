using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.UserPolicies.GetByPolicyId
{
    public class UserPoliciesGetByPolicyIdQuery : BaseQuery, IHandleableRequest<UserPoliciesGetByPolicyIdQuery, UserPoliciesGetByPolicyIdQueryHandler,
        UserPoliciesGetByPolicyIdQueryResult>
    {
        public Guid PolicyId { get; set; }
        //public IEnumerable<Claim> GetRequiredClaims()
        //{
        //    return new List<Claim> { new(this.GetType().Name, this.GetType().Name), };
        //}

        public UserPoliciesGetByPolicyIdQuery() : base(typeof(UserPoliciesGetByPolicyIdQuery))
        {
            
        }
    }
}
