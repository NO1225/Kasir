
using Kasir.Application.AppInfos.Queries.GetAppInfo;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Routes;
using Kasir.Application.Words.Queries.GetWords;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Api
{
    [AllowAnonymous]
    public class AppInfoController : BaseApiController
    {
        [HttpGet(ApiRoutes.AppInfo.Info)]
        public async Task<ActionResult<ServiceResult<AppInfoDto>>> GetAppInfo([FromQuery] GetAppInfoQuery query, CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(query, cancellationToken));
        }

    }
}
