using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL.Entites
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; } 
        public ICollection<Course>? Courses { get; set; } 
    }
}
