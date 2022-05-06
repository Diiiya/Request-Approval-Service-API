using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Api.UserRequests.Create
{
    public class UserRequestCreateCommandHandler : IRequestHandler<UserRequestCreateCommand, UserRequestCreateCommandResult>
    {
        private readonly IContextUserRequestsCommandsRepository _repository;

        public UserRequestCreateCommandHandler(IContextUserRequestsCommandsRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserRequestCreateCommandResult> Handle(UserRequestCreateCommand request,
            CancellationToken cancellationToken)
        {
            List<Domain.UserRequests> userRequests = new List<Domain.UserRequests>();

            foreach (var userId in request.UserIds)
            {
                var userRequest = new Domain.UserRequests()
                {
                    UserId = userId,
                    RequestId = request.RequestId
                };
                userRequests.Add(userRequest);
            }

            await this._repository.UserPolicies.AddRange(userRequests, cancellationToken);
            await this._repository.Complete(cancellationToken);

            var result = new UserRequestCreateCommandResult()
            {
                IsSuccess = true,
                RequestsCount = userRequests.Count
            };

            return result;
        }
    }
}
