using System;
using System.Collections.Generic;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.UserPolicies.Create
{
    public class UserPolicyCreateCommand : BaseCommand, IHandleableRequest<UserPolicyCreateCommand,
        UserPolicyCreateCommandHandler, UserPolicyCreateCommandResult>
    {
        public List<Guid> UserIds { get; set; }
        public Guid PolicyId { get; set; }
        public UserPolicyCreateCommand() : base(typeof(UserPolicyCreateCommand))
        {
            
        }
    }
}
