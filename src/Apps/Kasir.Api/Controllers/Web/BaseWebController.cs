using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kasir.Api.Controllers.Web
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class BaseWebController : Controller
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}