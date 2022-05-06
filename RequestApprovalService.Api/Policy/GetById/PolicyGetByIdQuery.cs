using System;
using RequestApprovalService.Silverspoon.Mediator;

namespace RequestApprovalService.Api.Policy.GetById
{
    public class PolicyGetByIdQuery : BaseQuery, IHandleableRequest<PolicyGetByIdQuery, PolicyGetByIdQueryHandler,
        PolicyGetByIdQueryResult>
    {
        public Guid Id { get; set; }

        public PolicyGetByIdQuery() : base(typeof(PolicyGetByIdQuery))
        {

        }
    }
}
