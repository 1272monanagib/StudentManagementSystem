using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL.Entites
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? AcademicYearId { get; set; }
        public AcademicYear? AcademicYear { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
