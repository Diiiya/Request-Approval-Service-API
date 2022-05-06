using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserPolicies;

namespace RequestApprovalService.Persistence.UserPolicies
{
    public class ContextUserPoliciesQueriesRepository : IContextUserPoliciesQueriesRepository
    {
        private readonly IDataContext _context;
        public IUserPoliciesQueriesRepository UserPoliciesQueriesRepository { get; }

        public ContextUserPoliciesQueriesRepository(IDataContext context)
        {
            _context = context;
            this.UserPoliciesQueriesRepository = new UserPoliciesQueriesRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
