using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NSwag.Annotations;
using RequestApprovalService.Api.User.GetAll;
using RequestApprovalService.Contracts;
using RequestApprovalService.Silverspoon.Mediator;
using RequestApprovalService.Api.User.GetById;
using RequestApprovalService.Api.User.Login;
using RequestApprovalService.Silverspoon.ExceptionBase;
using RequestApprovalService.Silverspoon.Pagination;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Controller
{
    [ApiController]
    [Route("/api/v1/Users")]
    public class UserController : MediatorRootController
    {
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var command = new UserGetByIdQuery()
            {
                Id = id,
            };

            var commandResult = await this.Mediator.Send(command);

            var result = commandResult.Match(exception => exception.ToActionResult(), model =>
                new ObjectResult(new UserGetAllResponse
                    {
                        Email = model.Email,
                        Username = model.Username,
                    })
                    { StatusCode = 200, });

            return this.Ok(result);
        }

        [SwaggerResponse(204, null)]
        [SwaggerResponse(400, typeof(BadRequestResponse))]
        [SwaggerResponse(404, typeof(NotFoundResponse))]
        [SwaggerResponse(500, typeof(InternalServerErrorResponse))]
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest request)
        {
            var command = new UserLoginCommand()
            {
                Username = request.Username,
                Password = request.Password,
            };

            var commandResult = await this.Mediator.Send(command);

            var result = commandResult.Match(exception => exception.ToActionResult(), model =>
                new ObjectResult(new UserLoginResponse
                    {
                        IsSuccess = model.IsSuccess,
                        UserId = model.UserId,
                        IsAdmin = model.isAdmin
                    })
                    { StatusCode = 200, });

            return this.Ok(result);
        }

        [HttpGet("GetAll/{page:int}/{pageLength:int}/{includeInvisible:bool}/{includeSoftDeleted:bool}")]
        public async Task<ActionResult> GetAll(int page = 1, int pageLength = 1000, bool includeInvisible = false,
            bool includeSoftDeleted = false)
        {
            var command = new UserGetAllQuery
            {
                Page = page,
                PageLength = pageLength,
                IncludeInvisible = includeInvisible,
                IncludeSoftDeleted = includeSoftDeleted,
            };

            var commandResult = await this.Mediator.Send(command);

            var result = commandResult.Match(exception => exception.ToActionResult(), model =>
                new ObjectResult(new ApiPagingViewModel<UserGetAllResponse>
                    {
                        Data = model.Data.Select(model => new UserGetAllResponse
                        {
                            UserId = model.UserId,
                            Username = model.Username,
                            Email = model.Email,
                            IsAdmin = model.IsAdmin,
                        }).ToList(),
                        Total = model.Total,
                    })
                    { StatusCode = 200, });

            return this.Ok(result);
        }
    }
}
