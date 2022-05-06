using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Request.Update
{
    public class RequestUpdateCommand : BaseQuery, IHandleableRequest<RequestUpdateCommand, RequestUpdateCommandHandler,
        RequestUpdateCommandResult>
    {
        public Guid Id { get; set; }
        public bool UserAnswer { get; set; }
        public RequestUpdateCommand() : base(typeof(RequestUpdateCommand))
        {
            
        }
    }
}
