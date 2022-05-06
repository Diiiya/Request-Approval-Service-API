using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Share;

namespace RequestApprovalService.Persistence.Share
{
    public class ContextShareQueriesRepository : IContextShareQueriesRepository
    {
        private readonly IDataContext _context;
        public IShareQueriesRepository ShareQueriesRepository { get; }

        public ContextShareQueriesRepository(IDataContext context)
        {
            _context = context;
            this.ShareQueriesRepository = new ShareQueriesRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
