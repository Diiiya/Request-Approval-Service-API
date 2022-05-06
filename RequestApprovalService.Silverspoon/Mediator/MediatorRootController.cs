using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RequestApprovalService.Silverspoon.Mediator
{
    public class MediatorRootController : ControllerBase
    {
        protected IMediator Mediator =>
            (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator)) ??
            throw new InvalidOperationException("MediatorRootController->Mediator is null");
    }

}
