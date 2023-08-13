using Microsoft.AspNetCore.Identity;

namespace TestSmth2.Repos
{
    public interface IUserRepo
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
