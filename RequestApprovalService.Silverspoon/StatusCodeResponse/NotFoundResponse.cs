using Microsoft.AspNetCore.Mvc.Filters;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.StatusCodeResponse.Base;

namespace RequestApprovalService.Silverspoon.StatusCodeResponse
{
    public class NotFoundResponse : BaseHttpStatusCodesResponses
    {
        public NotFoundResponse(EitherException exception) : base(exception)
        {
        }

        public NotFoundResponse(ExceptionContext context) : base(context)
        {
        }
    }

}
