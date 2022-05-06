using RequestApprovalService.Silverspoon.Pagination.Abstractions;
using System.Collections.Generic;

namespace RequestApprovalService.Silverspoon.Pagination
{
    public class DomainPagingViewModel<T> : IDomainPagingViewModel<T>
    {
        public DomainPagingViewModel(T data, int total)
        {
            Data = new List<T>
            {
                data
            };

            Total = total;
        }

        public DomainPagingViewModel(T data)
        {
            Data = new List<T>
            {
                data
            };

            Total = 1;
        }

        public DomainPagingViewModel(List<T> data, int total)
        {
            Data = data;
            Total = total;
        }

        public DomainPagingViewModel(List<T> data)
        {
            Data = data;
            Total = data.Count;
        }

        public DomainPagingViewModel()
        {
            Data = new List<T>();
            Total = 0;
        }

        public int Total { get; set; }
        public IList<T> Data { get; set; }
    }

}
