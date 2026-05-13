using StudentManagementSystem.BL.Dtos.CourseDtos;
using System.Collections.Generic;

namespace StudentManagementSystem.BL.Interface
{
    public interface ICourseService
    {
        void CreateCourse(CreateCourseDto dto);
        List<CourseDto> GetAllCourses();
        GetCourseDto GetCourseById(int id);
        void UpdateCourse(CourseDto dto);
        void DeleteCourse(int id);
    }
}
