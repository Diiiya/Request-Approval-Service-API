using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Share;

namespace RequestApprovalService.Persistence.Share
{
    public class ShareCommandsRepository : IShareCommandsRepository
    {
        private readonly IDataContext _context;

        public ShareCommandsRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.Share> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Shares.FirstOrDefaultAsync(x => x.PolicyId == id,
                    cancellationToken);
            return result;
        }

        public async Task Add(Domain.Share entity, CancellationToken cancellationToken)
        {
            await this._context.Shares.AddAsync(entity, cancellationToken);
        }

        public async Task AddRange(IEnumerable<Domain.Share> entities, CancellationToken cancellationToken)
        {
            await this._context.Shares.AddRangeAsync(entities, cancellationToken);
        }
    }
}
