using TestSmth2.Models.Domain;

namespace TestSmth2.Models.ViewModels
{
    public class PatientViewModel
    {
        public List<Patient> Patients { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientCNP { get; set; }
        public PatientViewModel()
        {
        }
    }
}