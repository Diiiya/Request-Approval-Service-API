using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.UserRequests
{
    public interface IContextUserRequestsCommandsRepository
    {
        IUserRequestsCommandsRepository UserPolicies { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
