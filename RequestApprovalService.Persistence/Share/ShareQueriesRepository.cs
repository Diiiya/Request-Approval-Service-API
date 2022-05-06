using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Share;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Persistence.Share
{
    public class ShareQueriesRepository : IShareQueriesRepository
    {
        private readonly IDataContext _context;
        public ShareQueriesRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.Share> FirstOrDefault(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Shares.FirstOrDefaultAsync(x => x.UserId == id,
                    cancellationToken);
            return result;
        }

        public Task<Domain.Share> GetExisting(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IDomainPagingViewModel<Domain.Share>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted, CancellationToken cancellationToken)
        {
            var resultCount = this._context.Shares.Count();
            var resultData = await this._context.Shares.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.Share>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }

        public async Task<Domain.Share> GetCodeByUserAndPolicyId(Guid userId, Guid policyId, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Shares.FirstOrDefaultAsync(x => x.PolicyId == policyId && x.UserId == userId,
                    cancellationToken);
            return result;
        }
    }
}
