using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kasir.Api.Controllers.Api
{
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer")]
    [ApiController]
    //[Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}