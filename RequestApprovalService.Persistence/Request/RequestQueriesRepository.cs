using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Repositories.Request;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Persistence.Request
{
    public class RequestQueriesRepository : IRequestQueriesRepository
    {
        private readonly IDataContext _context;
        public RequestQueriesRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.Request> FirstOrDefault(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Requests.FirstOrDefaultAsync(x => x.RequestId == id,
                    cancellationToken);
            return result;
        }

        public async Task<Domain.Request> GetExisting(string name, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Requests.FirstOrDefaultAsync(x => x.Name == name,
                    cancellationToken);
            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.Request>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted, CancellationToken cancellationToken)
        {
            var resultCount = this._context.Requests.Count();
            var resultData = await this._context.Requests.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.Request>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }
    }
}
