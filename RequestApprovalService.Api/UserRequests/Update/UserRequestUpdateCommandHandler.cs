using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Api.UserRequests.Update
{
    public class UserRequestUpdateCommandHandler : IRequestHandler<UserRequestUpdateCommand, UserRequestUpdateCommandResult>
    {
        private readonly IContextUserRequestsQueriesRepository _repository;

        public UserRequestUpdateCommandHandler(IContextUserRequestsQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserRequestUpdateCommandResult> Handle(UserRequestUpdateCommand request, CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.UserRequestsQueriesRepository.GetByUserAndRequestIds(request.RequestId, request.UserId, cancellationToken);
            
            repositoryResult.UserAnswer ??= request.UserAnswer;

            await this._repository.Complete(cancellationToken);

            var result = new UserRequestUpdateCommandResult
            {
                IsSuccess = true
            };

            return result;
        }
    }
}
