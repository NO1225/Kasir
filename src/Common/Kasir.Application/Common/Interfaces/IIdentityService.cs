using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Domain.Enums;

namespace Kasir.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUserDto> CheckUserPassword(string userName, string password, bool logIn = false);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<bool> UserIsInRole(string userId, string role);

        Task<IEnumerable<UserRole>> GetUserRolesAsync(string userId);

        Task<Result> DeleteUserAsync(string userId);
        bool IsSignedIn(ClaimsPrincipal principal);
        Task SignOutAsync();
    }
}
