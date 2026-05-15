using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.PL.ViewModel.Student
{
    public class CreateStudentVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(15 , 80)]
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearId { get; set; }
        public List<int> SelectedCourseIds { get; set; } 
    }
}
