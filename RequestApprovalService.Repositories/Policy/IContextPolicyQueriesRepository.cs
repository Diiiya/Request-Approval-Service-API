using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Policy
{
    public interface IContextPolicyQueriesRepository
    {
        IPolicyQueriesRepository PolicyQueriesRepository { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
