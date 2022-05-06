using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Persistence.UserRequests
{
    public class ContextUserRequestsCommandsRepository : IContextUserRequestsCommandsRepository
    {
        private readonly IDataContext _context;
        public IUserRequestsCommandsRepository UserPolicies { get; set; }

        public ContextUserRequestsCommandsRepository(IDataContext context)
        {
            _context = context;
            UserPolicies = new UserRequestsCommandsRepository(context);
        }

        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
