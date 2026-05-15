using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Dtos.StudentDtos
{
    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearId { get; set; }
        public List<int> CourseIds { get; set; }
    }
}
