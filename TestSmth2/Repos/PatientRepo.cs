using TestSmth2.Data;
using TestSmth2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace TestSmth2.Repos
{
    public class PatientRepo : IPatientRepo
    {
        private readonly MedicalDbContext context;
        public PatientRepo(MedicalDbContext context)
        {
            this.context = context;
        }
        public async Task<Patient> AddAsync(Patient Patient)
        {
            await context.AddAsync(Patient);
            await context.SaveChangesAsync();
            return Patient;
        }

        public async Task<Patient> DeleteAsync(string PatientCNP)
        {
            var target = await context.Patients.FindAsync(PatientCNP);
            if (target != null)
            {
                context.Patients.Remove(target);
                await context.SaveChangesAsync();
                return target;
            }
            return null;
        }

        public Task<IEnumerable<Patient>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await context.Patients.Include(x => x.Entries).ToListAsync();
        }

        public async Task<Patient> GetAsync(string PatientCNP)
        {
            return await context.Patients.Include(x => x.Entries).FirstOrDefaultAsync(y => y.PatientCNP.Equals(PatientCNP));
        }

        public async Task<IEnumerable<Patient>> SearchByCNP(string PatientCNP)
        {
            var patients = context.Patients.FromSqlRaw("SELECT * FROM Patients WHERE PatientCNP Like '" + PatientCNP + "%'").ToList();
            return patients;
        }

        public async Task<Patient> UpdateAsync(Patient Patient)
        {
            var target = await context.Patients.Include(x => x.Entries).FirstOrDefaultAsync(y => y.PatientCNP == Patient.PatientCNP);
            if (target != null)
            {
                target.PatientCNP = Patient.PatientCNP;
                target.PatientFirstName = Patient.PatientFirstName;
                target.PatientLastName = Patient.PatientLastName;
                target.Entries = Patient.Entries;
                await context.SaveChangesAsync();
                return target;
            }
            return null;
        }
    }
}
