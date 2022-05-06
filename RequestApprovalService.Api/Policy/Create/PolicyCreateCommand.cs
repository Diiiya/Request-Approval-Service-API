using System.Collections.Generic;
using RequestApprovalService.Domain;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Policy.Create
{
    //public class PolicyCreateCommand : BaseCommand, IHandleableRequest<PolicyCreateCommand, PolicyCreateCommandHandler, Either<PolicyCreateCommandResult>>
    public class PolicyCreateCommand : BaseCommand, IHandleableRequest<PolicyCreateCommand, PolicyCreateCommandHandler, PolicyCreateCommandResult>
    {
        public string Name { get; set; }
        public int Threshold { get; set; }
        public int Secret { get; set; }
        //public IEnumerable<UserPolicies> UserPolicies { get; set; }

        public PolicyCreateCommand() : base(typeof(PolicyCreateCommand))
        {
            
        }
    }
}
