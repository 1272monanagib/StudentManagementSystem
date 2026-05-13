using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Dtos.CourseDtos
{
    public class GetCourseDto
    {
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}
