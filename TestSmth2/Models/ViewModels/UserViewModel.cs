using System.ComponentModel.DataAnnotations;
using TestSmth2.ValidationTags;
using TestSmth2.Models.Domain;

namespace TestSmth2.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
        [HasSpecialChars(ErrorMessage = "Password has to contain at least 1 special character")]
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }


}
