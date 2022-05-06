using System;

namespace RequestApprovalService.Silverspoon.Mediator
{
    public class BaseQueryWithPaging : BaseQuery
    {
        private readonly int _page = 1;
        private readonly int _pageLength = 50;

        public int Skip => (this.Page - 1) * this.PageLength;
        public int Take => this.PageLength;

        public int Page
        {
            get => this._page;
            init =>
                this._page = value > 0
                    ? value
                    : throw new ArgumentOutOfRangeException($"Page must by strictly positive was '{value}'");
        }

        public int PageLength
        {
            get => this._pageLength;
            init =>
                this._pageLength = value > 0
                    ? value
                    : throw new ArgumentOutOfRangeException($"PageLength must by strictly positive was '{value}'");
        }

        public BaseQueryWithPaging(Type query) : base(query)
        {
        }
    }

}
