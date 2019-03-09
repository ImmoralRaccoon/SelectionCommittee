using System.ComponentModel.DataAnnotations;

namespace SelectionCommittee.API.Models.Authentication
{
    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}