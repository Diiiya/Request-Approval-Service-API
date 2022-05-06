using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.User;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Api.User.GetById
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, Either<UserGetByIdQueryResult>>
    {
        private readonly IContextUserQueriesRepository _repository;

        public UserGetByIdQueryHandler(IContextUserQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Either<UserGetByIdQueryResult>> Handle(
            UserGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryResult = await this._repository.UserQueriesRepository.FirstOrDefault(request.Id, cancellationToken);

            if (repositoryResult == null)
            {
                return new Either<UserGetByIdQueryResult>(
                    new RootException<UserGetByIdQuery>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
                        "UserDoesNotExist"));
            }
            
            var result = new UserGetByIdQueryResult
            {
                Username = repositoryResult.Username,
                Email = repositoryResult.Email,
                Password = repositoryResult.Password,
                IsAdmin = repositoryResult.IsAdmin,
            };

            return new Either<UserGetByIdQueryResult>(result);
        }
    }
}
