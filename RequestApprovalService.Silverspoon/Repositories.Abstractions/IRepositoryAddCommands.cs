using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestApprovalService.Silverspoon.Repositories.Abstractions
{
    public interface IRepositoryAddCommands<TEntity> where TEntity : class
    {
        Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken);
        Task Add(TEntity entity, CancellationToken cancellationToken);
        Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    }

}
