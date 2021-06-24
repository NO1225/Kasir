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

namespace Kasir.Api.Controllers
{
    [Authorize]
    public class CountriesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<CountryDto>>>> GetAllCountries(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllCountriesQuery(), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<CountryDto>>> Create(CreateCountryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<CountryDto>>> Update(UpdateCountryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<CountryDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCountryCommand { Id = id }));
        }
    }
}
