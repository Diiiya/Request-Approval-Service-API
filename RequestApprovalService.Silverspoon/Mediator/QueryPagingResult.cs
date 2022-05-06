using System.Collections.Generic;

namespace RequestApprovalService.Silverspoon.Mediator
{
    public class QueryPagingResult<T>
    {
        public int Total { get; }
        public IList<T> Data { get; }

        public QueryPagingResult(T data, int total)
        {
            this.Data = new List<T>
            {
                data,
            };
            this.Total = total;
        }

        public QueryPagingResult(T data)
        {
            this.Data = new List<T>
            {
                data,
            };
            this.Total = 1;
        }

        public QueryPagingResult(List<T> data, int total)
        {
            this.Data = data;
            this.Total = total;
        }

        public QueryPagingResult(List<T> data)
        {
            this.Data = data;
            this.Total = data.Count;
        }

        public QueryPagingResult()
        {
            this.Data = new List<T>();
            this.Total = 0;
        }
    }

}
