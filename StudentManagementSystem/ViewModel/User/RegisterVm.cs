using System.ComponentModel.DataAnnotations;


namespace StudentManagementSystem.PL.ViewModel.User
{
    public class RegisterVm
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public String Role { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
