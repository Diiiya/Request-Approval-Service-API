using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Request
{
    public interface IContextRequestQueriesRepository
    {
        IRequestQueriesRepository RequestQueriesRepository { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
