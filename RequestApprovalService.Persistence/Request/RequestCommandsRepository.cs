using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Request;

namespace RequestApprovalService.Persistence.Request
{
    public class RequestCommandsRepository : IRequestCommandsRepository
    {
        private readonly IDataContext _context;

        public RequestCommandsRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.Request> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Requests.FirstOrDefaultAsync(x => x.RequestId == id,
                    cancellationToken);
            return result;
        }

        public async Task Add(Domain.Request entity, CancellationToken cancellationToken)
        {
            await this._context.Requests.AddAsync(entity, cancellationToken);
        }

        public Task AddRange(IEnumerable<Domain.Request> entities, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
