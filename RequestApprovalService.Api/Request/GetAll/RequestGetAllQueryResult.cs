using System;

namespace RequestApprovalService.Api.Request.GetAll
{
    public class RequestGetAllQueryResult
    {
        public Guid RequestId { get; set; }
        public string Name { get; set; }
        public bool Approved { get; set; }
    }
}
