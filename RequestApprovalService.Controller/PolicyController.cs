using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestApprovalService.Api.Policy.Create;
using RequestApprovalService.Api.Policy.GetAll;
using RequestApprovalService.Api.Share.Create;
using RequestApprovalService.Api.User.GetById;
using RequestApprovalService.Api.UserPolicies.Create;
using RequestApprovalService.Api.UserPolicies.GetByPolicyId;
using RequestApprovalService.Contracts;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Mediator;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.Shamir;

namespace RequestApprovalService.Controller
{
    [ApiController]
    [Route("/api/v1/Policies")]
    public class PolicyController : MediatorRootController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PolicyCreateRequest request)
        {
            Random rnd = new Random();

            // 1. Create Policy Object
            var command = new PolicyCreateCommand
            {
                Name = request.Name,
                Threshold = request.Threshold,
                Secret = rnd.Next(50, 900),
            };

            var createdPolicy = await this.Mediator.Send(command);
            //var result = commandResult.Match(exception => exception.ToActionResult(), model =>
            //    new ObjectResult(new PolicyCreateCommandResult
            //        {
            //            Id = model.Id,
            //        })
            //        { StatusCode = 200, });


            // 2. For Each Policy UserId, Create UserPolicy Object
            var userPolicyCommand = new UserPolicyCreateCommand
            {
                PolicyId = createdPolicy.Id,
                UserIds = request.UserIds
            };
            var userPolicyCommandResult = await this.Mediator.Send(userPolicyCommand);
            //var userPolicyResult = userPolicyCommandResult.Match(exception => exception.ToActionResult(), model =>
            //    new ObjectResult(new UserPolicyCreateCommandResult
            //    {
            //        //IsSuccess = model.IsSuccess
            //    })
            //    { StatusCode = 200, });

            // 3. Split secret with Shamir Secret Scheme
            List<Point> shares = ShamirSecretSharing.SplitSecret(createdPolicy.Secret,
                userPolicyCommandResult.UsersCount, createdPolicy.Threshold);

            // 4. Create a Share for each User with X, Y and code value
            // Params PolicyId, User Ids and X and Y from the shares
            var shareCommand = new ShareCreateCommand
            {
                UserIds = request.UserIds,
                PolicyId = createdPolicy.Id,
                Shares = shares
            };
            var shareCommandResult = await this.Mediator.Send(shareCommand);

            // 5. Email each User with the Share
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("name.of.service@gmail.com", "a6uaha9XEWHgG-J8*njE"),
                EnableSsl = true,
            };

            foreach (KeyValuePair<Guid, Decimal> userShare in shareCommandResult.UserShares)
            {
                // do something with entry.Value or entry.Key
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("name.of.service@gmail.com"),
                    Subject = $"Code for new Policy {request.Name}",
                    Body =
                        $"<h3>Hello!</h3>\r\n<p>There has been a new Policy created and you get a key for it: <b>{userShare.Value}</b></p>\r\n\r\n<p>Kind regards,</p\r\n<p>Company name</p>",
                    IsBodyHtml = true,
                };
                // Get Users email by Id
                var getUserCommand = new UserGetByIdQuery()
                {
                    Id = userShare.Key,
                };

                var getUserCommandResult = await this.Mediator.Send(getUserCommand);
                var result = getUserCommandResult.Match(exception => exception.ToActionResult(), model =>
                    new ObjectResult(new UserGetAllResponse
                        {
                            Email = model.Email,
                            Username = model.Username,
                        })
                        {StatusCode = 200,}) as ObjectResult;
                var recipient = result.Value.GetType().GetProperty("Email").GetValue(result.Value);
                mailMessage.To.Add($"{recipient}");

                smtpClient.Send(mailMessage);
            }



            //return result;
            return NoContent();
        }

        //// Just for TEST
        //[HttpGet("GetById/{id}")]
        //public async Task<ActionResult> GetById(Guid id)
        //{
        //    var command = new UserPoliciesGetByPolicyIdQuery()
        //    {
        //        PolicyId = id,
        //    };

        //    var commandResult = await this.Mediator.Send(command);

        //    var result = commandResult.Match(exception => exception.ToActionResult(), model =>
        //        new ObjectResult(new ApiPagingViewModel<PolicyGetAllResponse>
        //            {
        //                Data = model.Data.Select(model => new PolicyGetAllResponse
        //                {
        //                    UserId = model.UserId,
        //                }).ToList(),
        //                Total = model.Total,
        //            })
        //            { StatusCode = 200, });

        //    return this.Ok(result);
        //}

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var command = new PolicyGetAllQuery();

            var commandResult = await this.Mediator.Send(command);

            var result = commandResult.Match(exception => exception.ToActionResult(), model =>
                new ObjectResult(new ApiPagingViewModel<PolicyGetAllResponse>
                {
                    Data = model.Data.Select(model => new PolicyGetAllResponse
                    {
                        PolicyId = model.PolicyId,
                        Name = model.Name
                    }).ToList(),
                    Total = model.Total,
                })
                { StatusCode = 200, });

            return this.Ok(result);
        }
    }
}
