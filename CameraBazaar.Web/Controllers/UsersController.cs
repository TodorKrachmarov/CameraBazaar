namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Contracts;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Profile(string Username)
        {
            var user = this.userService.Profile(Username);

            if (user == null)
            {
                this.SetError($"The user {Username} does not exist!");
                return this.RedirectToHome();
            }

            return this.View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                this.SetError("Can not load the user you want to edit!");
                return this.RedirectToHome();
            }

            var hasPassword = await userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(ManageController.SetPassword));
            }
            
            var model = new EditUserViewModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                Password = user.PasswordHash
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                this.SetError("Can not load the user you want to edit!");
                return this.RedirectToHome();
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

            if (model.Email != user.Email)
            {
                var setEmailResult = await userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            if (model.Phone != user.PhoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, model.Phone);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }
            
            return RedirectToAction(nameof(Profile), new { Username= this.User.Identity.Name });
        }

        private IActionResult RedirectToHome() => RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
