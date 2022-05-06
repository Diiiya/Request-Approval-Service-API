using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Persistence.UserRequests
{
    public class UserRequestsCommandsRepository : IUserRequestsCommandsRepository
    {
        private readonly IDataContext _context;

        public UserRequestsCommandsRepository(IDataContext context)
        {
            _context = context;
        }
        public Task<Domain.UserRequests> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Domain.UserRequests entity, CancellationToken cancellationToken)
        {
            await this._context.UserRequests.AddAsync(entity, cancellationToken);
        }

        public async Task AddRange(IEnumerable<Domain.UserRequests> entities, CancellationToken cancellationToken)
        {
            await this._context.UserRequests.AddRangeAsync(entities, cancellationToken);
        }
    }
}
