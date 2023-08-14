using TestSmth2.Models.Domain;
using TestSmth2.Models.ViewModels;
using TestSmth2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TestSmth2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPatientController : Controller
    {
        private readonly IPatientRepo patientRepo;
        public AdminPatientController(IPatientRepo patientRepo)
        {
            this.patientRepo = patientRepo;
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var patients = await patientRepo.GetAllAsync();

            var patientViewModel = new PatientViewModel();
            patientViewModel.Patients = new List<Patient>();

            foreach (var patient in patients)
            {
                patientViewModel.Patients.Add(new Patient
                {
                    PatientCNP = patient.PatientCNP,
                    PatientFirstName = patient.PatientFirstName,
                    PatientLastName = patient.PatientLastName,
                });
            }

            return View(patientViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(PatientViewModel patientViewModel)
        {
            var patient = new Patient
            {
                PatientFirstName = patientViewModel.PatientFirstName,
                PatientLastName = patientViewModel.PatientLastName,
                PatientCNP = patientViewModel.PatientCNP,
                Entries = new List<Entry>()
            };
            var potpat = await patientRepo.GetAsync(patientViewModel.PatientCNP);
            if (potpat == null)
            {
                
                await patientRepo.AddAsync(patient);
            }
            return RedirectToAction("List", "AdminPatient");
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string PatientCNP)
        {
            var patient = await patientRepo.GetAsync(PatientCNP);
            if (patient != null)
            {
                var deletedpatient = await patientRepo.DeleteAsync(PatientCNP);
                if (deletedpatient != null)
                {
                    return RedirectToAction("List", "AdminPatient");
                }
            }
            return RedirectToAction("List", "AdminPatient");

        }
    }
}
