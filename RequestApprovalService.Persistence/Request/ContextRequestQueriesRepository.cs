using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Request;

namespace RequestApprovalService.Persistence.Request
{
    public class ContextRequestQueriesRepository : IContextRequestQueriesRepository
    {
        private readonly IDataContext _context;
        public IRequestQueriesRepository RequestQueriesRepository { get; }

        public ContextRequestQueriesRepository(IDataContext context)
        {
            _context = context;
            this.RequestQueriesRepository = new RequestQueriesRepository(context);
        }
        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
