using System;
using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Silverspoon.Repositories.Abstractions
{
    public interface IRepositoryQueries<TEntity> where TEntity : class
    {
        Task<TEntity> FirstOrDefault(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetExisting(string name, CancellationToken cancellationToken);

        Task<IDomainPagingViewModel<TEntity>> GetAll(int skip, int take, bool includeInvisible, bool includeSoftDeleted,
            CancellationToken cancellationToken);
    }

}
