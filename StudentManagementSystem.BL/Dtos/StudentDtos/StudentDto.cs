using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Dtos.StudentDtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public List<string> Courses { get; set; } = new List<string>();
    }
}
