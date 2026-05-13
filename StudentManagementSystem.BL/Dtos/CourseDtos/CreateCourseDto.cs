namespace StudentManagementSystem.BL.Dtos.CourseDtos
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public int InstructorId { get; set; }
    }
}
