using RequestApprovalService.Silverspoon.Repositories.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Share
{
    public interface IShareQueriesRepository : IRepositoryQueries<Domain.Share>
    {
        Task<Domain.Share> GetCodeByUserAndPolicyId(Guid policyId, Guid userId, CancellationToken cancellationToken);
    }
}
