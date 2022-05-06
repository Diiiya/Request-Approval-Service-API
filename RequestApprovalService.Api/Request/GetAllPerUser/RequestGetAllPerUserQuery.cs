using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Request.GetAllPerUser
{
    public class RequestGetAllPerUserQuery : BaseQueryWithPaging, IHandleableRequest<RequestGetAllPerUserQuery, RequestGetAllPerUserQueryHandler,
        RequestGetAllPerUserQueryResult>
    {
        public Guid UserId { get; set; }

        public RequestGetAllPerUserQuery() : base(typeof(RequestGetAllPerUserQuery))
        {
            
        }
    }
}
