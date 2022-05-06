using System.Collections.Generic;
using System.Security.Claims;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.User.Login
{
    public class UserLoginCommand : BaseCommand, IHandleableRequest<UserLoginCommand, UserLoginCommandHandler,
        Either<UserLoginCommandResult>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<Claim> GetRequiredClaims()
        {
            return new List<Claim> { new(this.GetType().Name, this.GetType().Name), };
        }

        public UserLoginCommand() : base(typeof(UserLoginCommand))
        {
        }
    }
}
