using System;
using Microsoft.AspNetCore.Mvc;
using RequestApprovalService.Silverspoon.StatusCodeResponse;
using RequestApprovalService.Silverspoon.StatusCodeResponse.Base;

namespace RequestApprovalService.Silverspoon.ExceptionBase
{
    public static class EitherExceptionToActionResultExtensions
    {
        public static IActionResult ToActionResult(this EitherException exception)
        {
            var httpStatusCode = BaseHttpStatusCodes.Status401Unauthorized;
            if (exception.Data.Contains("HttpStatusCode"))
            {
                httpStatusCode =
                    (BaseHttpStatusCodes) (exception.Data["HttpStatusCode"] ?? throw new InvalidOperationException());
            }

            IBaseHttpStatusCodesResponses? switchResult = null;

            switch (httpStatusCode)
            {
                case BaseHttpStatusCodes.Status400BadRequest:
                    switchResult = new BadRequestResponse(exception);
                    break;
                //case BaseHttpStatusCodes.Status409Conflict:
                //    switchResult = new ConflictResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status424FailedDependency:
                //    switchResult = new FailedDependencyResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status498InvalidToken:
                //    switchResult = new InvalidTokenResponse(exception);
                //    break;
                case BaseHttpStatusCodes.Status404NotFound:
                    switchResult = new NotFoundResponse(exception);
                    break;
                //case BaseHttpStatusCodes.Status501NotImplemented:
                //    switchResult = new NotImplementedResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status412PreconditionFailed:
                //    switchResult = new PreconditionFailedResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status428PreconditionRequired:
                //    switchResult = new PreconditionFailedResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status499TokenRequired:
                //    switchResult = new TokenRequiredResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status429TooManyRequests:
                //    switchResult = new TooManyRequestsResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status401Unauthorized:
                //    switchResult = new UnauthorizedResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status406NotAcceptable:
                //    switchResult = new NotAcceptableResponse(exception);
                //    break;
                //case BaseHttpStatusCodes.Status403Forbidden:
                //    switchResult = new ForbiddenResponse(exception);
                //    break;
                default:
                    return new InternalServerErrorResponse(exception);
            }

            return switchResult.GetActionResult();
        }

    }
}
