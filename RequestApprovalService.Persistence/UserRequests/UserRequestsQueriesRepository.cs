using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.UserRequests;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Persistence.UserRequests
{
    public class UserRequestsQueriesRepository : IUserRequestsQueriesRepository
    {
        private readonly IDataContext _context;
        public UserRequestsQueriesRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.UserRequests> FirstOrDefault(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.UserRequests.FirstOrDefaultAsync(x => x.UserId == id,
                    cancellationToken);
            return result;
        }

        public Task<Domain.UserRequests> GetExisting(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IDomainPagingViewModel<Domain.UserRequests>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted, CancellationToken cancellationToken)
        {
            var resultCount = this._context.UserRequests.Count();
            var resultData = await this._context.UserRequests.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.UserRequests>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }

        public async Task<Domain.UserRequests> GetByUserAndRequestIds(Guid requestId, Guid userId, CancellationToken cancellationToken)
        {
            var result =
                await this._context.UserRequests.FirstOrDefaultAsync(x => x.RequestId == requestId && x.UserId == userId,
                    cancellationToken);
            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.UserRequests>> GetAllUsersByRequestId(Guid requestId, CancellationToken cancellationToken)
        {
            var resultCount = this._context.UserRequests.Where(r => r.RequestId == requestId && r.UserAnswer == true).Count();
            var resultData = await this._context.UserRequests.Where(r => r.RequestId == requestId && r.UserAnswer == true).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.UserRequests>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.UserRequests>> GetAllByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var resultCount = this._context.UserRequests.Where(r => r.UserId == userId).Count();
            var resultData = await this._context.UserRequests.Where(r => r.UserId == userId).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.UserRequests>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }
    }
}
