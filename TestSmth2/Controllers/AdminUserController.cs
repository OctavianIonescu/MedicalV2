using TestSmth2.Models.Domain;
using TestSmth2.Models.ViewModels;
using TestSmth2.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TestSmth2.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUserController(IUserRepo userRepo, UserManager<IdentityUser> userManager)
        {
            this.userRepo = userRepo;
            this.userManager = userManager;
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
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };


            var identityResult =
                await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    // assign roles to this user
                    var roles = new List<string> { "User" };

                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    identityResult =
                        await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUser");
                    }

                }
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
