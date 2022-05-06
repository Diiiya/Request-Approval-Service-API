using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Request;

namespace RequestApprovalService.Api.Request.Update
{
    public class RequestUpdateCommandHandler : IRequestHandler<RequestUpdateCommand, RequestUpdateCommandResult>
    {
        private readonly IContextRequestQueriesRepository _repository;

        public RequestUpdateCommandHandler(IContextRequestQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<RequestUpdateCommandResult> Handle(RequestUpdateCommand request, CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.RequestQueriesRepository.FirstOrDefault(request.Id, cancellationToken);

            repositoryResult.Approved = true;

            await this._repository.Complete(cancellationToken);

            var result = new RequestUpdateCommandResult
            {
                Request = repositoryResult
            };

            return result;
        }
    }
}
