using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Request
{
    public interface IContextRequestCommandsRepository
    {
        IRequestCommandsRepository Requests { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
