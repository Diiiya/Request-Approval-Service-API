using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.Share
{
    public interface IContextShareQueriesRepository
    {
        IShareQueriesRepository ShareQueriesRepository { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
