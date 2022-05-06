using System;
using System.Collections.Generic;
using System.Security.Claims;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.User.GetById
{
    public class UserGetByIdQuery : BaseQuery, IHandleableRequest<UserGetByIdQuery, UserGetByIdQueryHandler,
        Either<UserGetByIdQueryResult>>
    {
        public Guid Id { get; set; }
        public IEnumerable<Claim> GetRequiredClaims()
        {
            return new List<Claim> { new(this.GetType().Name, this.GetType().Name), };
        }

        public UserGetByIdQuery() : base(typeof(UserGetByIdQuery))
        {
        }
    }
}
