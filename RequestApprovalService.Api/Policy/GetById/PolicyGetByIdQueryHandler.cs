using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Policy;

namespace RequestApprovalService.Api.Policy.GetById
{
    public class PolicyGetByIdQueryHandler : IRequestHandler<PolicyGetByIdQuery, PolicyGetByIdQueryResult>
    {
        private readonly IContextPolicyQueriesRepository _repository;

        public PolicyGetByIdQueryHandler(IContextPolicyQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<PolicyGetByIdQueryResult> Handle(
            PolicyGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.PolicyQueriesRepository.FirstOrDefault(request.Id, cancellationToken);

            //if (repositoryResult == null)
            //{
            //    return new Either<PolicyGetByIdQueryResult>(
            //        new RootException<PolicyGetByIdQuery>(this.GetType(), request,
            //            BaseHttpStatusCodes.Status409Conflict,
            //            "PolicyDoesNotExist"));
            //}

            var result = new PolicyGetByIdQueryResult
            {
                Name = repositoryResult.Name,
                Secret = repositoryResult.Secret,
                Threshold = repositoryResult.Threshold
            };

            return result;
        }
    }
}
