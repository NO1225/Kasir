using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Countries.Commands.Create;
using Kasir.Application.Countries.Commands.Delete;
using Kasir.Application.Countries.Commands.Update;
using Kasir.Application.Countries.Queries.GetCountries;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kasir.Api.Controllers.Api
{
    [Authorize]
    public class CountriesController : BaseApiController
    {
        [HttpGet("/All")]
        public async Task<ActionResult<ServiceResult<List<CountryDto>>>> GetAllCountries(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllCountriesQuery(), cancellationToken));
        }

        [HttpPost("/Create")]
        public async Task<ActionResult<ServiceResult<CountryDto>>> Create(CreateCountryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("/Put")]
        public async Task<ActionResult<ServiceResult<CountryDto>>> Update(UpdateCountryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("Del/{id}")]
        public async Task<ActionResult<ServiceResult<CountryDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCountryCommand { Id = id }));
        }
    }
}
