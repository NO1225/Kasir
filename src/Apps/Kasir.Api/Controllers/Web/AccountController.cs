using Kasir.Application.ApplicationUser.Queries.GetToken;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Helpers;
using Kasir.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Web
{
    public class AccountController : BaseWebController
    {
        private readonly IIdentityService identityService;
        private readonly ICurrentUserService currentUserService;

        public AccountController(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            this.identityService = identityService;
            this.currentUserService = currentUserService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signIn)
        {
            var result = await Mediator.Send(new GetTokenQuery()
            {
                Email = signIn.Email,
                Password = signIn.Password
            });

            if(result.Succeeded == false)
            {
                ModelState.AddModelError("Email", "البريد الالكتروني وكلمة المرور لا يتطابقان");
                return View(signIn);
            }

            return RedirectToAction("index", "Home");

        }

        public async Task<IActionResult> SignOut()
        {
            await identityService.SignOutAsync();

            return RedirectToAction("index", "Home");
        }

        [Authorize(Roles = AccessRoles.Admin + "," + AccessRoles.Developer)]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            return View();
        }

        [Authorize(Roles = AccessRoles.Admin + "," + AccessRoles.Developer)]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = AccessRoles.Admin + "," + AccessRoles.Developer)]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c=>c.Type == "sub")?.Value;
                var result = await identityService.ChangePasswordAsync(userId, changePassword.OldPassword, changePassword.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Manage));
                }
                ModelState.AddModelError("OldPassword", string.Join('\n', result.Errors.Select(e => e.Description)));
            }
            return View(changePassword);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Recover(string email, string token)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Recover(RecoverPasswordViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = await userManager.FindByEmailAsync(viewModel.Email);
            //    var result = await userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.NewPassword);

            //    if (result.Succeeded)
            //    {
            //        return RedirectToAction(nameof(Confirmation));
            //    }
            //    ModelState.AddModelError("OldPassword", string.Join('\n', result.Errors.Select(e => e.Description)));
            //}
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }

    }
}
