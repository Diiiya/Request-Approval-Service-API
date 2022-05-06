using System.Collections.Generic;
using System.Security.Claims;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Api.Policy.GetAll
{
    public class PolicyGetAllQuery : BaseQueryWithPaging, IHandleableRequest<PolicyGetAllQuery, PolicyGetAllQueryHandler,
        Either<IQueryPagingViewModel<PolicyGetAllQueryResult>>>
    {
        public bool IncludeInvisible { get; set; } = false;
        public bool IncludeSoftDeleted { get; set; } = false;
        public IEnumerable<Claim> GetRequiredClaims()
        {
            return new List<Claim> { new(this.GetType().Name, this.GetType().Name), };
        }

        public PolicyGetAllQuery() : base(typeof(PolicyGetAllQuery))
        {
        }
    }
}
