using Microsoft.AspNetCore.Mvc.Filters;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.StatusCodeResponse.Base;

namespace RequestApprovalService.Silverspoon.StatusCodeResponse
{
    public class BadRequestResponse : BaseHttpStatusCodesResponses
    {
        public BadRequestResponse(EitherException exception) : base(exception)
        {
        }

        public BadRequestResponse(ExceptionContext context) : base(context)
        {
        }
    }

}
