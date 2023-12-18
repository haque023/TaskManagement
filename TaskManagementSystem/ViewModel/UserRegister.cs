using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.ViewModel
{
    public class UserRegister
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string ConfirmPassword { get; set; }
    }
}
