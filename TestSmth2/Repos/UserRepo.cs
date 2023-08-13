using TestSmth2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TestSmth2.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly MedAuthDbContext context;
        public UserRepo(MedAuthDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await context.Users.ToListAsync();
            var superAdminUser = await context.Users.FirstOrDefaultAsync(x => x.Email == "simonaionescu50@yahoo.com");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}
