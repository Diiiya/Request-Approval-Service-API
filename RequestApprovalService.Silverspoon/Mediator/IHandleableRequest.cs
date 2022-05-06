using MediatR;

namespace RequestApprovalService.Silverspoon.Mediator
{
    public interface IHandleableRequest<TRequest, THandler, out TResponse> : IRequest<TResponse>
        where TRequest : IRequest<TResponse> where THandler : IRequestHandler<TRequest, TResponse>
    {
    }

}
