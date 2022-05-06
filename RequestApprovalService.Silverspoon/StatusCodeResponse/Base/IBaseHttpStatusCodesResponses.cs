using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RequestApprovalService.Silverspoon.StatusCodeResponse.Base
{
    public interface IBaseHttpStatusCodesResponses
    {
        string? Text { get; set; }

        Task ExecuteResultAsync(ActionContext context);
        ActionResult GetActionResult();
    }

}
