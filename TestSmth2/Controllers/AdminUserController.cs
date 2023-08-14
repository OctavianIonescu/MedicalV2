using TestSmth2.Models.Domain;
using TestSmth2.Models.ViewModels;
using TestSmth2.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;

namespace TestSmth2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEmailSender emailSender;

        public AdminUserController(IUserRepo userRepo, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            this.userRepo = userRepo;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepo.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(user.Id),
                    userName = user.UserName,
                    eMail = user.Email
                });
            }

            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel model)
        {
            
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if (model.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    result =
                        await userManager.AddToRolesAsync(user, roles);
                    // Generate email confirmation token
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Generate email confirmation link
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    // Send email confirmation link to user
                    await emailSender.SendEmailAsync(model.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>link</a>");

                    return RedirectToAction("RegistrationConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            return RedirectToAction("List", "AdminUser");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user is not null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUser");
                }
            }

            return View();
        }
    }
}
