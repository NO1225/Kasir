using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Routes;
using Kasir.Application.Languages.Queries.GetLanguages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Api
{
    [AllowAnonymous]
    public class LanguagesController : BaseApiController
    {
        [HttpGet(ApiRoutes.Languages.GetAll)]
        public async Task<ActionResult<ServiceResult<List<LanguageDto>>>> GetAllLanguages(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllLanguagesQuery() { }, cancellationToken));
        }

    }
}
