using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Request;

namespace RequestApprovalService.Api.Request.GetById
{
    public class RequestGetByIdQueryHandler : IRequestHandler<RequestGetByIdQuery,
        RequestGetByIdQueryResult>
    {
        private readonly IContextRequestQueriesRepository _repository;

        public RequestGetByIdQueryHandler(IContextRequestQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<RequestGetByIdQueryResult> Handle(
            RequestGetByIdQuery request, CancellationToken cancellationToken)
        {
            var requestsList = new List<Domain.Request>();
            foreach (var reqId in request.RequestIds)
            {
                var repositoryResult = await this._repository.RequestQueriesRepository.FirstOrDefault(reqId, cancellationToken);
                requestsList.Add(repositoryResult);
            }

            var result = new RequestGetByIdQueryResult()
            {
                UserRequests = requestsList
            };

            return result;
        }
    }
}
