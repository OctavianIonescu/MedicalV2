using TestSmth2.Models.Domain;
using TestSmth2.Models.ViewModels;
using TestSmth2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TestSmth2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAntibioticController : Controller
    {
        private readonly IAntiBioticRepo antiBioticRepo;

        public AdminAntibioticController(IAntiBioticRepo antiBioticRepo)
        {
            this.antiBioticRepo = antiBioticRepo;
        }

        private void ValidateAddAntibioticRequest(AddAntibioticRequest req)
        {
            if(req.name == null)
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
        public async Task<IActionResult> Add(AddAntibioticRequest req)
        {
            ValidateAddAntibioticRequest(req);
            if(ModelState.IsValid == false)
            {
                return View();
            }
            var ab = new AntiBiotic
            {
                name = req.name + " :Sensitive",
                IsResistant = false
            };
            var ab1 = new AntiBiotic
            {
                name = req.name + " :Resistant",
                IsResistant = true

            };
            await antiBioticRepo.AddAsync(ab);
            await antiBioticRepo.AddAsync(ab1);
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var abs = await antiBioticRepo.GetAllAsync();
            return View(abs);
        }
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult>Edit(Guid ID)
        {
            var ab = await antiBioticRepo.GetAsync(ID);
            if(ab != null)
            {
                var req = new EditAntibioticRequest
                {
                    ID = ab.ID,
                    Name = ab.name,
                    IsResistant = ab.IsResistant
                };
                return View(req);
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditAntibioticRequest req)
        {
            var ab = new AntiBiotic
            {
                ID = req.ID,
                name = req.Name,
                IsResistant = req.IsResistant
            };
            var uab = await antiBioticRepo.UpdateAsync(ab);
            if(uab != null) 
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = req.ID });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditAntibioticRequest req)
        {
            var delab = await antiBioticRepo.DeleteAsync(req.ID);
            if (delab != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new {id = req.ID});
            }
        }

    }
}
