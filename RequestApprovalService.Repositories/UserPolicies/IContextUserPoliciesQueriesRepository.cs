using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.UserPolicies
{
    public interface IContextUserPoliciesQueriesRepository
    {
        IUserPoliciesQueriesRepository UserPoliciesQueriesRepository { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
