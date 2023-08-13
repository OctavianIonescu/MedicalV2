using Azure;
using TestSmth2.Data;
using TestSmth2.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace TestSmth2.Repos
{
    public class AntiBioticRepo : IAntiBioticRepo
    {
        private readonly MedicalDbContext _context;
        public AntiBioticRepo(MedicalDbContext context)
        {
            _context = context;
        }
        async Task<AntiBiotic> IAntiBioticRepo.AddAsync(AntiBiotic AntiBiotic)
        {
            await _context.AntiBiotics.AddAsync(AntiBiotic);
            await _context.SaveChangesAsync();
            return AntiBiotic;
        }

        async Task<AntiBiotic?> IAntiBioticRepo.DeleteAsync(Guid id)
        {
            var targetAntiBiotic = await _context.AntiBiotics.FindAsync(id);
            if (targetAntiBiotic != null)
            {
                _context.AntiBiotics.Remove(targetAntiBiotic);
                await _context.SaveChangesAsync();
                return targetAntiBiotic;
            }
            return null;
        }

        async Task<IEnumerable<AntiBiotic>> IAntiBioticRepo.GetAllAsync()
        {
            return await _context.AntiBiotics.ToListAsync();
        }

        Task<AntiBiotic?> IAntiBioticRepo.GetAsync(Guid id)
        {
            return _context.AntiBiotics.FirstOrDefaultAsync(t => t.ID == id);
        }

        async Task<AntiBiotic?> IAntiBioticRepo.UpdateAsync(AntiBiotic AntiBiotic)
        {
            var targetAntiBiotic = await _context.AntiBiotics.FindAsync(AntiBiotic.ID);
            if (targetAntiBiotic != null)
            {
                targetAntiBiotic.name = AntiBiotic.name;
                targetAntiBiotic.IsResistant = AntiBiotic.IsResistant;
                await _context.SaveChangesAsync();
                return targetAntiBiotic;
            }
            return null;
        }
    }
}
