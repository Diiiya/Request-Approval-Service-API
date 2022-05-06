using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.UserPolicies
{
    public interface IContextUserPoliciesCommandsRepository
    {
        IUserPoliciesCommandsRepository UserPolicies { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
