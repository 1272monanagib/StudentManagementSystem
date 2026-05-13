using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.PL.ViewModel.Instructor
{
    public class InstructorVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
