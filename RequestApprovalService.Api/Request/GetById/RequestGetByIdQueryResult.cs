using System.Collections.Generic;

namespace RequestApprovalService.Api.Request.GetById
{
    public class RequestGetByIdQueryResult
    {
        public List<Domain.Request> UserRequests { get; set; }
    }
}
