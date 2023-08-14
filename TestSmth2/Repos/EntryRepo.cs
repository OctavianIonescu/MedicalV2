using TestSmth2.Data;
using TestSmth2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace TestSmth2.Repos
{
    public class EntryRepo : IEntryRepo
    {
        private readonly MedicalDbContext context;
        public EntryRepo(MedicalDbContext context)
        {
            this.context = context;
        }
        public async Task<Entry> AddAsync(Entry entry)
        {
            await context.AddAsync(entry);
            await context.SaveChangesAsync();
            return entry;
        }

        public async Task<Entry> DeleteAsync(Guid ID)
        {
            var target = await context.Entries.FindAsync(ID);
            if (target != null)
            {
                context.Entries.Remove(target);
                await context.SaveChangesAsync();
                return target;
            }
            return null;
        }

        public async Task<IEnumerable<Entry>> GetAllAsync()
        {
            return await context.Entries.Include(x => x.Tags).Include(y => y.Patient).Include(z => z.Resistance).ToListAsync();
        }

        public async Task<Entry> GetAsync(Guid ID)
        {
            return await context.Entries.Include(x => x.Tags).Include(y => y.Patient).Include(z => z.Resistance).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<Entry> GetByURLAsync(string URLHandle)
        {
            return await context.Entries.Include(x => x.Tags).Include(y => y.Patient).Include(z => z.Resistance).FirstOrDefaultAsync(x => x.URLHandle == URLHandle);
        }

        public async Task<Entry> UpdateAsync(Entry entry)
        {
            var target = await context.Entries.Include(x => x.Tags).Include(y => y.Patient).Include(z => z.Resistance).FirstOrDefaultAsync(y => y.ID == entry.ID);
            if (target != null)
            {
                target.ID = entry.ID;
                target.entryCode = entry.entryCode;
                target.Microbe = entry.Microbe;
                target.sectiaDeProvenienta = entry.sectiaDeProvenienta;
                target.shortDescription = entry.shortDescription;
                target.medicSolicitant = entry.medicSolicitant;
                target.FileURL = entry.FileURL;
                target.Type = entry.Type;
                target.URLHandle = entry.URLHandle;
                target.collectionDate = entry.collectionDate;
                target.validationDate = entry.validationDate;
                target.Tags = entry.Tags;
                target.Resistance = entry.Resistance;
                await context.SaveChangesAsync();
                return target;
            }
            return null;
        }
    }
}
