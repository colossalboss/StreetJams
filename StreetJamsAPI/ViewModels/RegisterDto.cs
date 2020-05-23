using System;
using System.ComponentModel.DataAnnotations;

namespace StreetJamsAPI.ViewModels
{
    public class RegisterDto
    {
        public RegisterDto()
        {
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType("Password")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [DataType("Password")]
        public string ConfirmPassword { get; set; }
    }
}
