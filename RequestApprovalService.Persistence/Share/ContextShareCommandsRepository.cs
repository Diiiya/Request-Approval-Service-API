using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Share;

namespace RequestApprovalService.Persistence.Share
{
    public class ContextShareCommandsRepository : IContextShareCommandsRepository
    {
        private readonly IDataContext _context;
        public IShareCommandsRepository Shares { get; }

        public ContextShareCommandsRepository(IDataContext context)
        {
            _context = context;
            this.Shares = new ShareCommandsRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
