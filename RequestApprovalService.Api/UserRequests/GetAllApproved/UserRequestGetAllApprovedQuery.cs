using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.UserRequests.GetAllApproved
{
    public class UserRequestGetAllApprovedQuery : BaseQuery, IHandleableRequest<UserRequestGetAllApprovedQuery, UserRequestGetAllApprovedQueryHandler,
        UserRequestGetAllApprovedQueryResult>
    {
        public Guid RequestId { get; set; }
        public UserRequestGetAllApprovedQuery() : base(typeof(UserRequestGetAllApprovedQuery))
        {
            
        }
    }
}
