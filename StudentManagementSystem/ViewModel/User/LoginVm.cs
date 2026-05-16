using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.PL.ViewModel.User
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
