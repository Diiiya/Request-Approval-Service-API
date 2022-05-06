using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using System.Collections.Generic;

namespace RequestApprovalService.Silverspoon.Pagination
{
    public class QueryPagingViewModel<T> : IQueryPagingViewModel<T>
    {
        public int Total { get; set; }
        public IList<T> Data { get; set; }

        public QueryPagingViewModel(T data, int total)
        {
            Data = new List<T>
            {
                data
            };

            Total = total;
        }

        public QueryPagingViewModel(T data)
        {
            Data = new List<T>
            {
                data
            };

            Total = 1;
        }

        public QueryPagingViewModel(List<T> data, int total)
        {
            Data = data;
            Total = total;
        }

        public QueryPagingViewModel(List<T> data)
        {
            Data = data;
            Total = data.Count;
        }

        public QueryPagingViewModel()
        {
            Data = new List<T>();
            Total = 0;
        }
    }

}
