using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Request;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Api.Request.GetAll
{
    class RequestGetAllQueryHandler : IRequestHandler<RequestGetAllQuery,
        Either<IQueryPagingViewModel<RequestGetAllQueryResult>>>
    {
        private readonly IContextRequestQueriesRepository _repository;

        public RequestGetAllQueryHandler(IContextRequestQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Either<IQueryPagingViewModel<RequestGetAllQueryResult>>> Handle(
            RequestGetAllQuery request, CancellationToken cancellationToken)
        {
            var repositoryResult = await this._repository.RequestQueriesRepository.GetAll(request.Skip, request.Take,
                request.IncludeInvisible, request.IncludeSoftDeleted,
                cancellationToken);

            var selectResult = new List<RequestGetAllQueryResult>();
            selectResult = repositoryResult.Data.Select(item => new RequestGetAllQueryResult()
            {
                RequestId = item.RequestId,
                Name = item.Name,
                Approved = item.Approved
            }).ToList();

            if (selectResult.Count == 0)
            {
                return new Either<IQueryPagingViewModel<RequestGetAllQueryResult>>(
                    new RootException<RequestGetAllQuery>(this.GetType(), request,
                        BaseHttpStatusCodes.Status409Conflict,
                        "NoneFound"));
            }

            var result = new QueryPagingViewModel<RequestGetAllQueryResult>
            {
                Data = selectResult,
                Total = repositoryResult.Total,
            };

            return new Either<IQueryPagingViewModel<RequestGetAllQueryResult>>(result);
        }
    }
}
