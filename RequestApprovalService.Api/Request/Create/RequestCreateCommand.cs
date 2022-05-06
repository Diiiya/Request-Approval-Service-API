using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Request.Create
{
    public class RequestCreateCommand : BaseCommand, IHandleableRequest<RequestCreateCommand, RequestCreateCommandHandler, RequestCreateCommandResult>
    {
        public string Name { get; set; }
        public Guid PolicyId { get; set; }
        public RequestCreateCommand() : base(typeof(RequestCreateCommand))
        {
            
        }
    }
}
