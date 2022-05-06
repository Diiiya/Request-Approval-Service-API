using System;
using System.Collections.Generic;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.UserRequests.Create
{
    public class UserRequestCreateCommand : BaseCommand, IHandleableRequest<UserRequestCreateCommand,
        UserRequestCreateCommandHandler, UserRequestCreateCommandResult>
    {
        public List<Guid> UserIds { get; set; }
        public Guid RequestId { get; set; }
        public UserRequestCreateCommand() : base(typeof(UserRequestCreateCommand))
        {
            
        }
    }
}
