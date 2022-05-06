using System;
using System.Collections.Generic;

namespace RequestApprovalService.Api.UserRequests.GetAllApproved
{
    public class UserRequestGetAllApprovedQueryResult
    {
        public List<Guid> UserIds { get; set; }
    }
}
