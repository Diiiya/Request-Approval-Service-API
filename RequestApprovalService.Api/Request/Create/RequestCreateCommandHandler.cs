using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Request;

namespace RequestApprovalService.Api.Request.Create
{
    public class RequestCreateCommandHandler : IRequestHandler<RequestCreateCommand, RequestCreateCommandResult>
    {
        private readonly IContextRequestCommandsRepository _repository;
        private readonly IContextRequestQueriesRepository _repositoryQueries;

        public RequestCreateCommandHandler(IContextRequestCommandsRepository repository, IContextRequestQueriesRepository repositoryQueries)
        {
            _repository = repository;
            _repositoryQueries = repositoryQueries;
        }

        public async Task<RequestCreateCommandResult> Handle(RequestCreateCommand request,
            CancellationToken cancellationToken)
        {
            //var repositoryResult =
            //    await _repositoryQueries.RequestQueriesRepository.GetExisting(request.Name, cancellationToken);

            //if (repositoryResult != null)
            //{
            //    return new Either<RequestCreateCommandResult>(
            //        new RootException<RequestCreateCommand>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
            //            "RequestAlreadyExists"));
            //}

            var entity = new Domain.Request()
            {
                RequestId = Guid.NewGuid(),
                Name = request.Name,
                PolicyId = request.PolicyId,
                Approved = false,
            };

            await this._repository.Requests.Add(entity, cancellationToken);
            await this._repository.Complete(cancellationToken);

            var result = new RequestCreateCommandResult()
            {
                Id = entity.RequestId,
            };

            return result;
        }
    }
}
