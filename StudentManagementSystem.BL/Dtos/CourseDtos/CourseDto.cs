namespace StudentManagementSystem.BL.Dtos.CourseDtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}
