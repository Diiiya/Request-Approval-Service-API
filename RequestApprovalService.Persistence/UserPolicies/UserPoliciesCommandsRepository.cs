using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;

namespace RequestApprovalService.Persistence.UserPolicies
{
    public class UserPoliciesCommandsRepository : Repositories.UserPolicies.IUserPoliciesCommandsRepository
    {
        private readonly IDataContext _context;

        public UserPoliciesCommandsRepository(IDataContext context)
        {
            _context = context;
        }
        public Task<Domain.UserPolicies> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Domain.UserPolicies entity, CancellationToken cancellationToken)
        {
            await this._context.UserPolicies.AddAsync(entity, cancellationToken);
        }

        public async Task AddRange(IEnumerable<Domain.UserPolicies> entities, CancellationToken cancellationToken)
        {
            await this._context.UserPolicies.AddRangeAsync(entities, cancellationToken);
        }
    }
}
