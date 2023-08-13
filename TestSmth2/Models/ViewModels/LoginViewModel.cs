﻿using System.ComponentModel.DataAnnotations;

namespace TestSmth2.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string? ReturnURL { get; set; }
    }
}
