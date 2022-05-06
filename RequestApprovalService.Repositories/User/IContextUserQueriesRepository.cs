using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.User
{
    public interface IContextUserQueriesRepository
    {
        IUserQueriesRepository UserQueriesRepository { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
