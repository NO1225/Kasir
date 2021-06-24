using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;

namespace Kasir.Application.ApplicationUser.Queries.GetToken
{
    public class GetTokenQuery :IRequestWrapper<LoginResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class GetTokenQueryHandler : IRequestHandlerWrapper<GetTokenQuery, LoginResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public GetTokenQueryHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<LoginResponse>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.CheckUserPassword(request.Email, request.Password, true);

            if (user == null)
                return ServiceResult.Failed<LoginResponse>(ServiceError.ForbiddenError);


            return ServiceResult.Success(new LoginResponse
            {
                User = user,
                Token = await _tokenService.CreateJwtSecurityTokenAsync(user.Id)
            });
        }

    }
}
