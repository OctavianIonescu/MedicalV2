using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TestSmth2.ValidationTags;

namespace TestSmth2.Models.ViewModels
{
    public class RegisterViewModel: PageModel
    {
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
