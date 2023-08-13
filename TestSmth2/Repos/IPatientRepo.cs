using TestSmth2.Models.Domain;

namespace TestSmth2.Repos
{
    public interface IPatientRepo
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> GetAsync(string PatientCNP);
        Task<IEnumerable<Patient>> GetAll();
        Task<Patient> AddAsync(Patient Patient);
        Task<Patient> UpdateAsync(Patient Patient);
        Task<Patient> DeleteAsync(string PatientCNP);
        Task<IEnumerable<Patient>> SearchByCNP(string PatientCNP);
    }
}
