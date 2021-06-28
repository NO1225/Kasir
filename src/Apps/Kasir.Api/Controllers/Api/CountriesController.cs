using Kasir.Application.Common.Models;
using Kasir.Application.Countries.Queries.GetCountries;
using Kasir.Application.Dto;
using Kasir.Application.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Api
{
    [AllowAnonymous]
    public class CountriesController : BaseApiController
    {
        [HttpGet(ApiRoutes.Countries.GetAll)]
        public async Task<ActionResult<ServiceResult<List<CountryDto>>>> GetAllCountries([FromQuery]GetAllCountriesQuery query,CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(query, cancellationToken));
        }

    }
}
