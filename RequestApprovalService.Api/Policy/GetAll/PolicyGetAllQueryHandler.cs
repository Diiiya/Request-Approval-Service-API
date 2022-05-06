using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Policy;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Api.Policy.GetAll
{
    public class PolicyGetAllQueryHandler : IRequestHandler<PolicyGetAllQuery,
        Either<IQueryPagingViewModel<PolicyGetAllQueryResult>>>
    {
        private readonly IContextPolicyQueriesRepository _repository;

        public PolicyGetAllQueryHandler(IContextPolicyQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Either<IQueryPagingViewModel<PolicyGetAllQueryResult>>> Handle(
            PolicyGetAllQuery request, CancellationToken cancellationToken)
        {
            var repositoryResult = await this._repository.PolicyQueriesRepository.GetAll(request.Skip, request.Take,
                request.IncludeInvisible, request.IncludeSoftDeleted,
                cancellationToken);

            var selectResult = new List<PolicyGetAllQueryResult>();
            selectResult = repositoryResult.Data.Select(item => new PolicyGetAllQueryResult()
            {
                PolicyId = item.PolicyId,
                Name = item.Name
            }).ToList();

            if (selectResult.Count == 0)
            {
                return new Either<IQueryPagingViewModel<PolicyGetAllQueryResult>>(
                    new RootException<PolicyGetAllQuery>(this.GetType(), request,
                        BaseHttpStatusCodes.Status409Conflict,
                        "NoneFound"));
            }

            var result = new QueryPagingViewModel<PolicyGetAllQueryResult>
            {
                Data = selectResult,
                Total = repositoryResult.Total,
            };

            return new Either<IQueryPagingViewModel<PolicyGetAllQueryResult>>(result);
        }
    }
}
