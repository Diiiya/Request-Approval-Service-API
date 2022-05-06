using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestApprovalService.Repositories.Share;

namespace RequestApprovalService.Api.Share.Create
{
    public class ShareCreateCommandHandler : IRequestHandler<ShareCreateCommand, ShareCreateCommandResult>
    {
        private readonly IContextShareCommandsRepository _repository;

        public ShareCreateCommandHandler(IContextShareCommandsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShareCreateCommandResult> Handle(ShareCreateCommand request, CancellationToken cancellationToken)
        {
            //var repositoryResult = await _repository.Shares.GetExisting(request.Name, cancellationToken);

            //if (repositoryResult != null)
            //{
            //    //return new Either<PolicyCreateCommandResult>(
            //    //    new RootException<PolicyCreateCommand>(this.GetType(), request, BaseHttpStatusCodes.Status409Conflict,
            //    //        "PolicyAlreadyExists"));
            //    return new ShareCreateCommandResult()
            //    {
            //        Id = Guid.Empty,
            //    };
            //}

            // List of User Ids
            // List of SHares X and Y
            // Get User 0 and match with Share 0

            List<Domain.Share> entities = new();
            Dictionary<Guid, Decimal> userShares = new();

            for (int i = 0; i < request.UserIds.Count; i++)
            {
                var entity = new Domain.Share()
                {
                    ShareId = Guid.NewGuid(),
                    X = request.Shares[i].X,
                    Y = request.Shares[i].Y,
                    PolicyId = request.PolicyId,
                    UserId = request.UserIds[i],
                };
                entities.Add(entity);
                userShares.Add(entity.UserId, entity.Y);
            }

            
            //foreach (var user in request.UserPolicies)
            //{
            //    user.PolicyId = entity.PolicyId;
            //}

            await this._repository.Shares.AddRange(entities, cancellationToken);
            await this._repository.Complete(cancellationToken);

            var result = new ShareCreateCommandResult()
            {
                //CreatedSharesCount = entities.Count
                UserShares = userShares
            };

            //return new Either<PolicyCreateCommandResult>(result);
            return result;
        }
    }
}
