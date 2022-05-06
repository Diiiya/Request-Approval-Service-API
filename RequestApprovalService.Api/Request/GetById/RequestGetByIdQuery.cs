using System;
using System.Collections.Generic;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Request.GetById
{
    public class RequestGetByIdQuery : BaseQueryWithPaging, IHandleableRequest<RequestGetByIdQuery, RequestGetByIdQueryHandler,
        RequestGetByIdQueryResult>
    {
        public List<Guid> RequestIds { get; set; }
        public RequestGetByIdQuery() : base(typeof(RequestGetByIdQuery))
        {
            
        }
    }
}
