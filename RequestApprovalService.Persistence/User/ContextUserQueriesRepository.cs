using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.User;

namespace RequestApprovalService.Persistence.User
{
    public class ContextUserQueriesRepository : IContextUserQueriesRepository
    {
        private readonly IDataContext _context;
        public IUserQueriesRepository UserQueriesRepository { get; }

        public ContextUserQueriesRepository(IDataContext context)
        {
            _context = context;
            this.UserQueriesRepository = new UserQueriesRepository(context);
        }

        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
