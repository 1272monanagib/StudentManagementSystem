using StudentManagementSystem.BL.Dtos.AcademicYearDtos;
using StudentManagementSystem.BL.Dtos.CourseDtos;
using StudentManagementSystem.BL.Dtos.DepartmentDtos;
using System.Collections.Generic;

namespace StudentManagementSystem.BL.Dtos.StudentDtos
{
    public class CreateStudentFormDataDto
    {
        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();
        public List<AcademicYearDto> AcademicYears { get; set; } = new List<AcademicYearDto>();
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
