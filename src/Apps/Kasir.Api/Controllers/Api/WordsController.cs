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
    public class WordsController : BaseApiController
    {
        [HttpGet(ApiRoutes.Words.GetAll)]
        public async Task<ActionResult<ServiceResult<List<WordDto>>>> GetAllWords(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllWordsQuery() { LanguageId = 2, CountryId = 1 }, cancellationToken));
        }

    }
}
