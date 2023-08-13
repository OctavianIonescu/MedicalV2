using TestSmth2.Models.Domain;

namespace TestSmth2.Repos
{
    public interface IEntryRepo
    {
        Task<IEnumerable<Entry>> GetAllAsync();
        Task<Entry> GetAsync(Guid ID);
        Task<Entry> GetByURLAsync(string URLHandle);
        Task<Entry> AddAsync(Entry entry);
        Task<Entry> UpdateAsync(Entry entry);
        Task<Entry> DeleteAsync(Guid ID);
    }
}
