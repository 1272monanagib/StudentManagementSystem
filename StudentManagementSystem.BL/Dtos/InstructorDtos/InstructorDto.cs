
namespace StudentManagementSystem.BL.Dtos.InstructorDtos
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
