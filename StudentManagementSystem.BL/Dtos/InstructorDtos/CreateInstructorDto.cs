
namespace StudentManagementSystem.BL.Dtos.InstructorDtos
{
    public class CreateInstructorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
} 
