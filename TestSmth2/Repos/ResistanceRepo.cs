using Microsoft.EntityFrameworkCore;
using TestSmth2.Data;
using TestSmth2.Models.Domain;

namespace TestSmth2.Repos
{
    public class ResistanceRepo : IResistanceRepo
    {
        private readonly MedicalDbContext _context;
        public ResistanceRepo(MedicalDbContext context)
        {
            _context = context;
        }
        async Task<ResistanceMechanism> IResistanceRepo.AddAsync(ResistanceMechanism Resistance)
        {
            await _context.ResistanceMechanisms.AddAsync(Resistance);
            await _context.SaveChangesAsync();
            return Resistance;
        }

        async Task<ResistanceMechanism?> IResistanceRepo.DeleteAsync(Guid id)
        {
            var targetResistance = await _context.ResistanceMechanisms.FindAsync(id);
            if (targetResistance != null)
            {
                _context.ResistanceMechanisms.Remove(targetResistance);
                await _context.SaveChangesAsync();
                return targetResistance;
            }
            return null;
        }

        async Task<IEnumerable<ResistanceMechanism>> IResistanceRepo.GetAllAsync()
        {
            return await _context.ResistanceMechanisms.ToListAsync();
        }

        Task<ResistanceMechanism?> IResistanceRepo.GetAsync(Guid id)
        {
            return _context.ResistanceMechanisms.FirstOrDefaultAsync(t => t.ID == id);
        }

        async Task<ResistanceMechanism?> IResistanceRepo.UpdateAsync(ResistanceMechanism Resistance)
        {
            var targetResistance = await _context.ResistanceMechanisms.FindAsync(Resistance.ID);
            if (targetResistance != null)
            {
                targetResistance.Name = Resistance.Name;
                await _context.SaveChangesAsync();
                return targetResistance;
            }
            return null;
        }
    }
}

