using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserPolicies;

namespace RequestApprovalService.Persistence.UserPolicies
{
    public class ContextUserPoliciesCommandsRepository : IContextUserPoliciesCommandsRepository
    {
        private readonly IDataContext _context;
        public IUserPoliciesCommandsRepository UserPolicies { get; }

        public ContextUserPoliciesCommandsRepository(IDataContext context)
        {
            _context = context;
            this.UserPolicies = new UserPoliciesCommandsRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
