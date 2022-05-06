using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserPolicies;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Persistence.UserPolicies
{
    public class UserPoliciesQueriesRepository : IUserPoliciesQueriesRepository
    {
        private readonly IDataContext _context;
        public UserPoliciesQueriesRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.UserPolicies> FirstOrDefault(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.UserPolicies.FirstOrDefaultAsync(x => x.UserId == id,
                    cancellationToken);
            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.UserPolicies>> GetAllUsersByPolicyId(Guid id, CancellationToken cancellationToken)
        {
            var resultCount = this._context.UserPolicies.Where(p => p.PolicyId == id).Count();
            var resultData = await this._context.UserPolicies.Where(p => p.PolicyId == id).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.UserPolicies>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.UserPolicies>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted, CancellationToken cancellationToken)
        {
            var resultCount = this._context.UserPolicies.Count();
            var resultData = await this._context.UserPolicies.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.UserPolicies>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }

        public Task<Domain.UserPolicies> GetExisting(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
