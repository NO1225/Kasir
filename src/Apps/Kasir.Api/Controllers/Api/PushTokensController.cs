using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.PushTokens.Commands.Create;
using Kasir.Application.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Api
{
    [AllowAnonymous]
    public class PushTokensController : BaseApiController
    {
        [HttpPost(ApiRoutes.Auth.AddPushToken)]
        public async Task<ActionResult<ServiceResult<PushTokenDto>>> AddPushToken([FromBody] CreatePushTokenCommand command, CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(command, cancellationToken));
        }

    }
}
