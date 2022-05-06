using System.Collections.Generic;

namespace RequestApprovalService.Silverspoon.Pagination.Abstractions
{
    public interface IQueryPagingViewModel<T>
    {
        int Total { get; set; }
        IList<T> Data { get; set; }
    }

}
