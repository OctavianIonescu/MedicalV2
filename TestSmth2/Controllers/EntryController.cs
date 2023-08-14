using Microsoft.AspNetCore.Mvc;
using TestSmth2.Data;
using Microsoft.AspNetCore.Identity;
using TestSmth2.Repos;
using TestSmth2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TestSmth2.Controllers
{
    [Authorize(Roles = "User")]
    public class EntryController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEntryRepo entryRepo;

        public EntryController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IEntryRepo entryRepo)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.entryRepo = entryRepo;
        }

        public async Task<IActionResult> Index(string URLHandle)
        {
            if (signInManager.IsSignedIn(User))
            {
                var entry = await entryRepo.GetByURLAsync(URLHandle);
                //var entryDetails = new EntryDetailsViewModel();
                if(entry !=  null)
                {
                    return View(entry);
                }
            }
                
            return View();
        }
    }
}
