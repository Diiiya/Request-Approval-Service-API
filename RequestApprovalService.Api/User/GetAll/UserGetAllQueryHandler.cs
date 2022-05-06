using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.User;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Api.User.GetAll
{
    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery,
        Either<IQueryPagingViewModel<UserGetAllQueryResult>>>
    {
        private readonly IContextUserQueriesRepository _repository;

        public UserGetAllQueryHandler(IContextUserQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Either<IQueryPagingViewModel<UserGetAllQueryResult>>> Handle(
            UserGetAllQuery request, CancellationToken cancellationToken)
        {
            var repositoryResult = await this._repository.UserQueriesRepository.GetAll(request.Skip, request.Take,
                request.IncludeInvisible, request.IncludeSoftDeleted,
                cancellationToken);

            var selectResult = new List<UserGetAllQueryResult>();
            selectResult = repositoryResult.Data.Select(item => new UserGetAllQueryResult()
            {
                UserId = item.UserId,
                Username = item.Username,
                Email = item.Email,
                IsAdmin = item.IsAdmin,
            }).ToList();

            if (selectResult.Count == 0)
            {
                return new Either<IQueryPagingViewModel<UserGetAllQueryResult>>(
                    new RootException<UserGetAllQuery>(this.GetType(), request,
                        BaseHttpStatusCodes.Status409Conflict,
                        "NoneFound"));
            }

            var result = new QueryPagingViewModel<UserGetAllQueryResult>
            {
                Data = selectResult,
                Total = repositoryResult.Total,
            };

            return new Either<IQueryPagingViewModel<UserGetAllQueryResult>>(result);
        }
    }
}
