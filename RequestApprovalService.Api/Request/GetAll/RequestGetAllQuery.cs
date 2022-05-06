using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Api.Request.GetAll
{
    public class RequestGetAllQuery : BaseQueryWithPaging, IHandleableRequest<RequestGetAllQuery, RequestGetAllQueryHandler,
        Either<IQueryPagingViewModel<RequestGetAllQueryResult>>>
    {
        public bool IncludeInvisible { get; set; } = false;
        public bool IncludeSoftDeleted { get; set; } = false;
        public RequestGetAllQuery() : base(typeof(RequestGetAllQuery))
        {
            
        }
    }
}
