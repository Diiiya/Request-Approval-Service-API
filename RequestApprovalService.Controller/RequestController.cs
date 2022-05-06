using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestApprovalService.Api.Policy.GetById;
using RequestApprovalService.Api.Request.Create;
using RequestApprovalService.Api.Request.GetAll;
using RequestApprovalService.Api.Request.GetAllPerUser;
using RequestApprovalService.Api.Request.GetById;
using RequestApprovalService.Api.Request.Update;
using RequestApprovalService.Api.Share.GetByUserPolicy;
using RequestApprovalService.Api.UserPolicies.GetByPolicyId;
using RequestApprovalService.Api.UserRequests.Create;
using RequestApprovalService.Api.UserRequests.GetAllApproved;
using RequestApprovalService.Api.UserRequests.Update;
using RequestApprovalService.Contracts;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Shamir;

namespace RequestApprovalService.Controller
{
    [ApiController]
    [Route("/api/v1/Requests")]
    public class RequestController : MediatorRootController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RequestCreateRequest request)
        {
            // 1. Create Request Object
            var createRequestCommand = new RequestCreateCommand
            {
                Name = request.Name,
                PolicyId = request.PolicyId,
            };

            var createdRequest = await this.Mediator.Send(createRequestCommand);
            var createdRequestId = createdRequest.Id;
            //var createdRequestResult = createdRequest.Match(exception => exception.ToActionResult(), model =>
            //    new ObjectResult(new RequestCreateCommandResult
            //    {
            //        Id = model.Id,
            //    })
            //    { StatusCode = 200, }) as ObjectResult;

            //var createdRequestId = createdRequestResult.Value.GetType().GetProperty("Id").GetValue(createdRequestResult.Value);

            // 2. Get All the Users of the Policy
            var getPolicyUsersCommand = new UserPoliciesGetByPolicyIdQuery()
            {
                PolicyId = request.PolicyId,
            };

            var getPolicyUserIdsCommandResult = await this.Mediator.Send(getPolicyUsersCommand);
            
            // 3. Create a UserRequest for each User
            var userRequestCommand = new UserRequestCreateCommand
            {
                UserIds = getPolicyUserIdsCommandResult.UserIds,
                RequestId = (Guid) createdRequestId
            };
            var shareCommandResult = await this.Mediator.Send(userRequestCommand);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] RequestUpdateRequest request)
        {
            // 1. Get User Share for that Request and Policy 
            var getUserPolicyShareCommand = new ShareGetByUserPolicyQuery()
            {
                PolicyId = request.PolicyId,
                UserId = request.UserId
            };

            var getUserPolicyShareCommandResult = await this.Mediator.Send(getUserPolicyShareCommand);

            // 2. Check if User Code is a valid one - correct Policy, userid and share combination
            // request.Code == get a share.Y based on userId and policyId
            if (getUserPolicyShareCommandResult.Share.Y != request.Code)
            {
                return BadRequest();
            }

            // 3. Update UserRequest Answer to request.UserAnswer
            var updateUserRequestCommand = new UserRequestUpdateCommand()
            {
                RequestId = request.RequestId,
                UserId = request.UserId,
                UserAnswer = request.UserAnswer
            };

            var updateUserRequestCommandResult = await this.Mediator.Send(updateUserRequestCommand);


            // 4. Check if request.RequestId.Count == Policy.Threshold and if so get all Users Ids for that Policy
            var getApprovedRequestsCountQuery = new UserRequestGetAllApprovedQuery()
            {
                RequestId = request.RequestId
            };

            var getApprovedRequestsCountQueryResult = await this.Mediator.Send(getApprovedRequestsCountQuery);

            

            var getPolicyByIdQuery = new PolicyGetByIdQuery()
            {
                Id = request.PolicyId
            };

            var getPolicyByIdQueryResult = await this.Mediator.Send(getPolicyByIdQuery);
            int policyThreshold = getPolicyByIdQueryResult.Threshold;
           
            if (getApprovedRequestsCountQueryResult.UserIds.Count != policyThreshold)
            {
                return Ok();
            }

            // 5. Get those user ids -> user shares and Out of those shares
            List<Point> sharesToReconstruct = new();
            foreach (var userId in getApprovedRequestsCountQueryResult.UserIds)
            {
                var getUserPolicyShareCommand2 = new ShareGetByUserPolicyQuery()
                {
                    PolicyId = request.PolicyId,
                    UserId = userId
                };

                var getUserPolicyShareCommandResult2 = await this.Mediator.Send(getUserPolicyShareCommand2);
                sharesToReconstruct.Add(new Point
                {
                    X = getUserPolicyShareCommandResult2.Share.X,
                    Y = getUserPolicyShareCommandResult2.Share.Y
                });
            }

            // 6. Recontruct the Secret
            int reconstructedSecret = ShamirSecretSharing.ReconstructSecret(sharesToReconstruct);

            // 7. Check if the Secrets Match
            int policySecret = getPolicyByIdQueryResult.Secret;
            if (reconstructedSecret != policySecret)
            {
                return BadRequest();
            }

            // 7. If the Secrets match => Update the Request as Approved
            var updateRequestCommand = new RequestUpdateCommand()
            {
                Id = request.RequestId
            };

            var updateRequestCommandResult = await this.Mediator.Send(updateRequestCommand);

            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var command = new RequestGetAllQuery();

            var commandResult = await this.Mediator.Send(command);

            var result = commandResult.Match(exception => exception.ToActionResult(), model =>
                new ObjectResult(new ApiPagingViewModel<RequestGetAllResponse>
                    {
                        Data = model.Data.Select(model => new RequestGetAllResponse
                        {
                            RequestId = model.RequestId,
                            Name = model.Name,
                            Approved = model.Approved
                        }).ToList(),
                        Total = model.Total,
                    })
                    { StatusCode = 200, });

            return this.Ok(result);
        }

        [HttpGet("GetAllPerUser/{userId}")]
        public async Task<ActionResult> GetAllPerUser(Guid userId)
        {
            var command = new RequestGetAllPerUserQuery()
            {
                UserId = userId
            };

            var commandResult = await this.Mediator.Send(command);

            var requestsIds = commandResult.RequestIds;

            var getUserRequestsQuery = new RequestGetByIdQuery()
            {
                RequestIds = requestsIds
            };

            var getUserRequestsQueryResult = await this.Mediator.Send(getUserRequestsQuery);

            return this.Ok(getUserRequestsQueryResult);
        }
    }
}
