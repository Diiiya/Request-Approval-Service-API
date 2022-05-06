using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Policy;

namespace RequestApprovalService.Persistence.Policy
{
    public class PolicyCommandsRepository : IPolicyCommandsRepository
    {
        private readonly IDataContext _context;

        public PolicyCommandsRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.Policy> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Policies.FirstOrDefaultAsync(x => x.PolicyId == id,
                    cancellationToken);
            return result;
        }

        public async Task Add(Domain.Policy entity, CancellationToken cancellationToken)
        {
            await this._context.Policies.AddAsync(entity, cancellationToken);
        }

        public Task AddRange(IEnumerable<Domain.Policy> entities, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
