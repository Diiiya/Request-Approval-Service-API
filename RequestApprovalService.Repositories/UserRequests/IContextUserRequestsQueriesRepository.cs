using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Repositories.UserRequests
{
    public interface IContextUserRequestsQueriesRepository
    {
        IUserRequestsQueriesRepository UserRequestsQueriesRepository { get; }
        Task<int> Complete(CancellationToken cancellationToken);
    }
}
