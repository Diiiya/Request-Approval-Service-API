using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Policy;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Api.Policy.Create
{
    //public class PolicyCreateCommandHandler : IRequestHandler<PolicyCreateCommand, Either<PolicyCreateCommandResult>>
    public class PolicyCreateCommandHandler : IRequestHandler<PolicyCreateCommand, PolicyCreateCommandResult>
    {
        private readonly IContextPolicyCommandsRepository _repository;
        private readonly IContextPolicyQueriesRepository _repositoryQueries;

        public PolicyCreateCommandHandler(IContextPolicyCommandsRepository repository, IContextPolicyQueriesRepository repositoryQueries)
        {
            _repository = repository;
            _repositoryQueries = repositoryQueries;
        }

        //public async Task<Either<PolicyCreateCommandResult>> Handle(PolicyCreateCommand request, CancellationToken cancellationToken)
        public async Task<PolicyCreateCommandResult> Handle(PolicyCreateCommand request, CancellationToken cancellationToken)
        {
            var repositoryResult = await _repositoryQueries.PolicyQueriesRepository.GetExisting(request.Name, cancellationToken);

            if (repositoryResult != null)
            {
                //return new Either<PolicyCreateCommandResult>(
                //    new RootException<PolicyCreateCommand>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
                //        "PolicyAlreadyExists"));
                return new PolicyCreateCommandResult()
                {
                    Id = Guid.Empty,
                };
            }

            var entity = new Domain.Policy()
            {
                PolicyId = Guid.NewGuid(),
                Name = request.Name,
                Threshold = request.Threshold,
                Secret = request.Secret,
               // Users = request.UserPolicies
            };
            //foreach (var user in request.UserPolicies)
            //{
            //    user.PolicyId = entity.PolicyId;
            //}

            await this._repository.Policies.Add(entity, cancellationToken);
            await this._repository.Complete(cancellationToken);

            var result = new PolicyCreateCommandResult()
            {
                Id = entity.PolicyId,
                Secret = entity.Secret,
                Threshold = entity.Threshold
            };

            //return new Either<PolicyCreateCommandResult>(result);
            return result;
        }
    }
}
