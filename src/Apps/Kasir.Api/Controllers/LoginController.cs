using System.Threading.Tasks;
using Kasir.Application.ApplicationUser.Queries.GetToken;
using Kasir.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kasir.Api.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Create(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
