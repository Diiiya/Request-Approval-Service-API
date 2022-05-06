using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.UserPolicies;
using RequestApprovalService.Silverspoon.ExceptionBase;

namespace RequestApprovalService.Api.UserPolicies.Create
{
    public class UserPolicyCreateCommandHandler : IRequestHandler<UserPolicyCreateCommand, UserPolicyCreateCommandResult>
    {
        private readonly IContextUserPoliciesCommandsRepository _repository;

        public UserPolicyCreateCommandHandler(IContextUserPoliciesCommandsRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserPolicyCreateCommandResult> Handle(UserPolicyCreateCommand request, CancellationToken cancellationToken)
        {
            //var repositoryResult = await _repository.UserPolicies.FindAsync(request.PolicyId, cancellationToken);

            //if (repositoryResult != null)
            //{
            //    return new Either<UserPolicyCreateCommandResult>(
            //        new RootException<UserPolicyCreateCommand>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
            //            "PolicyAlreadyExists"));
            //}

            List<Domain.UserPolicies> userPolicies = new List<Domain.UserPolicies>();

            foreach (var userId in request.UserIds)
            {
                var userPolicy = new Domain.UserPolicies()
                {
                    UserId = userId,
                    PolicyId = request.PolicyId
                };
                userPolicies.Add(userPolicy);
            }

            await this._repository.UserPolicies.AddRange(userPolicies, cancellationToken);
            await this._repository.Complete(cancellationToken);

            var result = new UserPolicyCreateCommandResult()
            {
                IsSuccess = true,
                UsersCount = userPolicies.Count
            };

            return result;
        }
    }
}
