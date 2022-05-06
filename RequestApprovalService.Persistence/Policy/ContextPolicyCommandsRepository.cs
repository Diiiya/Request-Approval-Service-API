using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Policy;

namespace RequestApprovalService.Persistence.Policy
{
    public class ContextPolicyCommandsRepository : IContextPolicyCommandsRepository
    {
        private readonly IDataContext _context;
        public IPolicyCommandsRepository Policies { get; }

        public ContextPolicyCommandsRepository(IDataContext context)
        {
            _context = context;
            this.Policies = new PolicyCommandsRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
