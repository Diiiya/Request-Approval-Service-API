using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RequestApprovalService.Silverspoon.ExceptionBase;

namespace RequestApprovalService.Silverspoon.StatusCodeResponse
{
    public class InternalServerErrorResponse : IActionResult
    {
        private readonly Exception? _exception;


        public InternalServerErrorResponse(EitherException? exception)
        {
            _exception = exception;
        }

        public InternalServerErrorResponse(ExceptionContext? context)
        {
            if (context != null)
                _exception = context.Exception;
        }

        public IActionResult GetActionResult()
        {
#if (DEBUG)
            var data = _exception;
#elif (RELEASE)
                        var data = "Oops";
#endif

            var objectResult = new ObjectResult(data)
            {
                StatusCode = (int)BaseHttpStatusCodes.Status500InternalServerError
            };

            return objectResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = GetActionResult();
            await objectResult.ExecuteResultAsync(context);
        }
    }

}
