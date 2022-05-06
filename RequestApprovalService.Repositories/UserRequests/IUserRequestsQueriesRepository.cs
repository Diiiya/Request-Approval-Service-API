using System;
using System.Threading;
using System.Threading.Tasks;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using RequestApprovalService.Silverspoon.Repositories.Abstractions;

namespace RequestApprovalService.Repositories.UserRequests
{
    public interface IUserRequestsQueriesRepository : IRepositoryQueries<Domain.UserRequests>
    {
        Task<Domain.UserRequests> GetByUserAndRequestIds(Guid requestId, Guid userId, CancellationToken cancellationToken);
        Task<IDomainPagingViewModel<Domain.UserRequests>> GetAllUsersByRequestId(Guid requestId, CancellationToken cancellationToken);
        Task<IDomainPagingViewModel<Domain.UserRequests>> GetAllByUserId(Guid userId, CancellationToken cancellationToken);
    }
}
