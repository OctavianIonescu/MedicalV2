using Azure;
using TestSmth2.Models.Domain;

namespace TestSmth2.Repos
{
    public interface IAntiBioticRepo
    {
        Task<IEnumerable<AntiBiotic>> GetAllAsync();
        Task<AntiBiotic?> GetAsync(Guid id);
        Task<AntiBiotic> AddAsync(AntiBiotic antibiotic);
        Task<AntiBiotic?> UpdateAsync(AntiBiotic antibiotic);
        Task<AntiBiotic?> DeleteAsync(Guid id);
    }
}
