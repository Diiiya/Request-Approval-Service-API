using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Policy;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Persistence.Policy
{
    public class PolicyQueriesRepository : IPolicyQueriesRepository
    {
        private readonly IDataContext _context;
        public PolicyQueriesRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.Policy> FirstOrDefault(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Policies.FirstOrDefaultAsync(x => x.PolicyId == id,
                    cancellationToken);
            return result;
        }

        public async Task<Domain.Policy> GetExisting(string name, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Policies.FirstOrDefaultAsync(x => x.Name == name,
                    cancellationToken);
            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.Policy>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted, CancellationToken cancellationToken)
        {
            var resultCount = this._context.Policies.Count();
            var resultData = await this._context.Policies.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.Policy>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }
    }
}
