using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL.Entites
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; } = new Instructor();  
    }
}
