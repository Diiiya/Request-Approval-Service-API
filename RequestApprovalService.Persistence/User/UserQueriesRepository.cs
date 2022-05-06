using RequestApprovalService.Repositories.User;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestApprovalService.Domain;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Persistence.User
{
    public class UserQueriesRepository : IUserQueriesRepository
    {
        private readonly IDataContext _context;
        public UserQueriesRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<Domain.User> FirstOrDefault(Guid id, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Users.FirstOrDefaultAsync(x => x.UserId == id,
                    cancellationToken);
            return result;
        }

        public async Task<Domain.User> GetExisting(string name, CancellationToken cancellationToken)
        {
            var result =
                await this._context.Users.FirstOrDefaultAsync(x => x.Username == name,
                    cancellationToken);
            return result;
        }

        public async Task<IDomainPagingViewModel<Domain.User>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted, CancellationToken cancellationToken)
        {
            var resultCount = this._context.Users.Count();
            var resultData = await this._context.Users.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var result = new DomainPagingViewModel<Domain.User>
            {
                Data = resultData,
                Total = resultCount,
            };

            return result;
        }
    }
}
