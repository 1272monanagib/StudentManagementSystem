using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.PL.ViewModel.Department
{
    public class DepartmentVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentCount { get; set; }
        public int InstructorCount { get; set; }
    }
}
