using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL.Entites
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = new Student();
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();
        public double Grade { get; set; }
    }
}
