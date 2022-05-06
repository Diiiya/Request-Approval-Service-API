using System.Collections.Generic;

namespace RequestApprovalService.Silverspoon.Pagination.Abstractions
{
    public interface IApiPagingViewModel<T>
    {
        int Total { get; set; }
        IList<T> Data { get; set; }
    }

}
