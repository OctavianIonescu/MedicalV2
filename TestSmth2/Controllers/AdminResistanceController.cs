using Microsoft.AspNetCore.Mvc;
using TestSmth2.Models.Domain;
using TestSmth2.Models.ViewModels;
using TestSmth2.Repos;

namespace TestSmth2.Controllers
{
    public class AdminResistanceController : Controller
    {
        private readonly IResistanceRepo resistanceRepo;

        public AdminResistanceController(IResistanceRepo ResistanceRepo)
        {
            this.resistanceRepo = ResistanceRepo;
        }

        private void ValidateAddResistanceRequest(AddResistanceRequest req)
        {
            if (req.name == null)
            {
                ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddResistanceRequest req)
        {
            ValidateAddResistanceRequest(req);
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var ab = new ResistanceMechanism
            {
                Name = req.name,
            };
            await resistanceRepo.AddAsync(ab);
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var abs = await resistanceRepo.GetAllAsync();
            return View(abs);
        }
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid ID)
        {
            var ab = await resistanceRepo.GetAsync(ID);
            if (ab != null)
            {
                var req = new EditResistanceRequest
                {
                    ID = ab.ID,
                    Name = ab.Name
                };
                return View(req);
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditResistanceRequest req)
        {
            var ab = new ResistanceMechanism
            {
                ID = req.ID,
                Name = req.Name
            };
            var uab = await resistanceRepo.UpdateAsync(ab);
            if (uab != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = req.ID });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditResistanceRequest req)
        {
            var delab = await resistanceRepo.DeleteAsync(req.ID);
            if (delab != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = req.ID });
            }
        }

    }
}
