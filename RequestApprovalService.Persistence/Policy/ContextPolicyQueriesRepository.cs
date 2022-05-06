using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Policy;

namespace RequestApprovalService.Persistence.Policy
{
    public class ContextPolicyQueriesRepository : IContextPolicyQueriesRepository
    {
        private readonly IDataContext _context;
        public IPolicyQueriesRepository PolicyQueriesRepository { get; }

        public ContextPolicyQueriesRepository(IDataContext context)
        {
            _context = context;
            this.PolicyQueriesRepository = new PolicyQueriesRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
