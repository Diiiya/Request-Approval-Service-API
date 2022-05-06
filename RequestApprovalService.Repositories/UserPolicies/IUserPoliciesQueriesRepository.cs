using System;
using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using RequestApprovalService.Silverspoon.Repositories.Abstractions;

namespace RequestApprovalService.Repositories.UserPolicies
{
    public interface IUserPoliciesQueriesRepository : IRepositoryQueries<Domain.UserPolicies>
    {
        Task<IDomainPagingViewModel<Domain.UserPolicies>> GetAllUsersByPolicyId(Guid id, CancellationToken cancellationToken);
    }
}
