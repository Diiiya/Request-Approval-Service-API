using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.User;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Api.User.Login
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Either<UserLoginCommandResult>>
    {
        private readonly IContextUserQueriesRepository _repository;

        public UserLoginCommandHandler(IContextUserQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Either<UserLoginCommandResult>> Handle(
            UserLoginCommand request,
            CancellationToken cancellationToken)
        {
            var repositoryResult = await this._repository.UserQueriesRepository.GetExisting(request.Username, cancellationToken);

            if (repositoryResult == null)
            {
                return new Either<UserLoginCommandResult>(
                    new RootException<UserLoginCommand>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
                        "UserDoesNotExist"));
            }

            if (request.Password != repositoryResult.Password)
            {
                return new Either<UserLoginCommandResult>(
                    new RootException<UserLoginCommand>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
                        "Invalid credentials"));
            }

            var result = new UserLoginCommandResult()
            {
                IsSuccess = true,
                UserId = repositoryResult.UserId,
                isAdmin = repositoryResult.IsAdmin
            };

            return new Either<UserLoginCommandResult>(result);
        }
    }
}
