using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Request;

namespace RequestApprovalService.Persistence.Request
{
    public class ContextRequestCommandsRepository : IContextRequestCommandsRepository
    {
        private readonly IDataContext _context;
        public IRequestCommandsRepository Requests { get; }

        public ContextRequestCommandsRepository(IDataContext context)
        {
            _context = context;
            this.Requests = new RequestCommandsRepository(context);
        }

        public async Task<int> Complete(CancellationToken cancellationToken)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
