using System.ComponentModel.DataAnnotations;

namespace SelectionCommittee.API.Models.Authentication
{
    public class UserRegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirm { get; set; }

        [Required]
        public string[] Roles { get; set; }
    }
}