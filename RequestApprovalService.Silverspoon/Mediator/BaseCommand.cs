using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RequestApprovalService.Silverspoon.Mediator
{
    public class BaseCommand 
    {
        public virtual IEnumerable<Claim> GetRequiredClaims()
        {
            return new List<Claim>
            {
                new(_command.Name, _command.Name)
            };
        }

        private readonly Type _command;

        public BaseCommand(Type command)
        {
            _command = command;
        }
    }

}
