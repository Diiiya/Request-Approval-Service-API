using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.UserRequests.Update
{
    public class UserRequestUpdateCommand : BaseCommand, IHandleableRequest<UserRequestUpdateCommand,
        UserRequestUpdateCommandHandler, UserRequestUpdateCommandResult>
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public bool UserAnswer { get; set; }
        public UserRequestUpdateCommand(): base(typeof(UserRequestUpdateCommand))
        {
            
        }
    }
}
