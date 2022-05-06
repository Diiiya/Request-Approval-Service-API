using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Policy
{
    public interface IContextPolicyCommandsRepository
    {
        IPolicyCommandsRepository Policies { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
