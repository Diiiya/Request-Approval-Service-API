using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Api.UserRequests.GetAllApproved
{
    public class UserRequestGetAllApprovedQueryHandler : IRequestHandler<UserRequestGetAllApprovedQuery, UserRequestGetAllApprovedQueryResult>
    {
        private readonly IContextUserRequestsQueriesRepository _repository;

        public UserRequestGetAllApprovedQueryHandler(IContextUserRequestsQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserRequestGetAllApprovedQueryResult> Handle(
            UserRequestGetAllApprovedQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.UserRequestsQueriesRepository
                    .GetAllUsersByRequestId(request.RequestId, cancellationToken);

            var selectResult = new List<Guid>();
            selectResult = repositoryResult.Data.Select(item => item.UserId).ToList();
            

            var result = new UserRequestGetAllApprovedQueryResult
            {
                UserIds = selectResult
            };

            return result;
        }
    }
}
