using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Persistence.UserRequests
{
    public class ContextUserRequestsQueriesRepository : IContextUserRequestsQueriesRepository
    {
        private readonly IDataContext _context;
        public IUserRequestsQueriesRepository UserRequestsQueriesRepository { get; }

        public ContextUserRequestsQueriesRepository(IDataContext context)
        {
            _context = context;
            this.UserRequestsQueriesRepository = new UserRequestsQueriesRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
