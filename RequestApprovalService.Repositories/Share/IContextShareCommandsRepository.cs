using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Share
{
    public interface IContextShareCommandsRepository
    {
        IShareCommandsRepository Shares { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
