using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.PL.ViewModel.Department
{
    public class CreateDepartmentVm
    {
        [Required]
        public string Name { get; set; }
    }
}
