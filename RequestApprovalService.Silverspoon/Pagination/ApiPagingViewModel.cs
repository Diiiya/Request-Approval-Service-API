using System.Collections.Generic;
using RequestApprovalService.Silverspoon.Pagination.Abstractions;

namespace RequestApprovalService.Silverspoon.Pagination
{
    public class ApiPagingViewModel<T> : IApiPagingViewModel<T>
    {
        public ApiPagingViewModel(T data, int total)
        {
            this.Data = new List<T>
            {
                data,
            };
            this.Total = total;
        }

        public ApiPagingViewModel(T data)
        {
            this.Data = new List<T>
            {
                data,
            };
            this.Total = 1;
        }

        public ApiPagingViewModel(List<T> data, int total)
        {
            this.Data = data;
            this.Total = total;
        }

        public ApiPagingViewModel(List<T> data)
        {
            this.Data = data;
            this.Total = data.Count;
        }

        public ApiPagingViewModel()
        {
            this.Data = new List<T>();
            this.Total = 0;
        }

        public int Total { get; set; }
        public IList<T> Data { get; set; }
    }

}
