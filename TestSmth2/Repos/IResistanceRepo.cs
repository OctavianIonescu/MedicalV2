using TestSmth2.Models.Domain;

namespace TestSmth2.Repos
{
    public interface IResistanceRepo
    {
        Task<IEnumerable<ResistanceMechanism>> GetAllAsync();
        Task<ResistanceMechanism?> GetAsync(Guid id);
        Task<ResistanceMechanism> AddAsync(ResistanceMechanism ResistanceMechanism);
        Task<ResistanceMechanism?> UpdateAsync(ResistanceMechanism ResistanceMechanism);
        Task<ResistanceMechanism?> DeleteAsync(Guid id);
    }
}
