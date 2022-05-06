using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Share;

namespace RequestApprovalService.Api.Share.GetByUserPolicy
{
    public class ShareGetByUserPolicyQueryHandler : IRequestHandler<ShareGetByUserPolicyQuery, ShareGetByUserPolicyQueryResult>
    {
        private readonly IContextShareQueriesRepository _repository;

        public ShareGetByUserPolicyQueryHandler(IContextShareQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShareGetByUserPolicyQueryResult> Handle(
            ShareGetByUserPolicyQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryResult =
                await this._repository.ShareQueriesRepository
                    .GetCodeByUserAndPolicyId(request.UserId, request.PolicyId, cancellationToken);
            
            var result = new ShareGetByUserPolicyQueryResult
            {
                Share = repositoryResult
            };

            return result;
        }
    }
}
