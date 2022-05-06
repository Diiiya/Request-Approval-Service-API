using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.UserRequests;

namespace RequestApprovalService.Api.Request.GetAllPerUser
{
    public class RequestGetAllPerUserQueryHandler : IRequestHandler<RequestGetAllPerUserQuery,
        RequestGetAllPerUserQueryResult>
    {
        private readonly IContextUserRequestsQueriesRepository _repository;

        public RequestGetAllPerUserQueryHandler(IContextUserRequestsQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<RequestGetAllPerUserQueryResult> Handle(
            RequestGetAllPerUserQuery request, CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.UserRequestsQueriesRepository.GetAllByUserId(request.UserId, cancellationToken);

            //var selectResult = new List<RequestGetAllPerUserQueryResult>();
            var selectResult = repositoryResult.Data.Select(item => item.RequestId).ToList();
            
            var result = new RequestGetAllPerUserQueryResult
            {
                //Data = selectResult,
                //Total = repositoryResult.Total,
                RequestIds = selectResult
            };

            return result;
        }
    }
}
