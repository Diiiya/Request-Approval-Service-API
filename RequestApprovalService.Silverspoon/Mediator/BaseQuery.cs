using System;

namespace RequestApprovalService.Silverspoon.Mediator
{
    public class BaseQuery : BaseCommand
    {
        public BaseQuery(Type query) : base(query)
        {
        }
    }

}
