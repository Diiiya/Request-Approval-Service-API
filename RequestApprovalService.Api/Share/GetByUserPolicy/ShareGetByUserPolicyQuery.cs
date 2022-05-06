using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Share.GetByUserPolicy
{
    public class ShareGetByUserPolicyQuery : BaseQuery, IHandleableRequest<ShareGetByUserPolicyQuery, ShareGetByUserPolicyQueryHandler,
        ShareGetByUserPolicyQueryResult>
    {
        public Guid PolicyId { get; set; }
        public Guid UserId { get; set; }
        public ShareGetByUserPolicyQuery() : base(typeof(ShareGetByUserPolicyQuery))
        {
            
        }
    }
}
