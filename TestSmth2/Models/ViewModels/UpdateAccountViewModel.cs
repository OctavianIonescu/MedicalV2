using Microsoft.AspNetCore.Identity;

namespace TestSmth2.Models.ViewModels
{
    public class UpdateAccountViewModel
    {
        public IdentityUser user { get; set; }
        public string? Email { get; set; } = null;
        public string? UserName { get; set; } = null;
        public string? OldPassword { get; set; } = null;
        public string? Password { get; set; } = null;
    }
}
