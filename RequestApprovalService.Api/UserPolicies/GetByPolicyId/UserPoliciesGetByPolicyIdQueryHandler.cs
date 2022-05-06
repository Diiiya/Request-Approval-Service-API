using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.UserPolicies;

namespace RequestApprovalService.Api.UserPolicies.GetByPolicyId
{
    class UserPoliciesGetByPolicyIdQueryHandler : IRequestHandler<UserPoliciesGetByPolicyIdQuery, UserPoliciesGetByPolicyIdQueryResult>
    {
        private readonly IContextUserPoliciesQueriesRepository _repository;

        public UserPoliciesGetByPolicyIdQueryHandler(IContextUserPoliciesQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserPoliciesGetByPolicyIdQueryResult> Handle(
            UserPoliciesGetByPolicyIdQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.UserPoliciesQueriesRepository
                    .GetAllUsersByPolicyId(request.PolicyId, cancellationToken);

            var selectResult = new List<Guid>();
            selectResult = repositoryResult.Data.Select(item => item.UserId).ToList();

            //if (selectResult.Count == 0)
            //{
            //    return new UserPoliciesGetByPolicyIdQueryResult(
            //        new RootException<UserPoliciesGetByPolicyIdQuery>(this.GetType(), request,
            //            BaseHttpStatusCodes.Status409Conflict,
            //            "NoneFound"));
            //}

            var result = new UserPoliciesGetByPolicyIdQueryResult
            {
                UserIds = selectResult
            };

            return result;
        }
    }

}
