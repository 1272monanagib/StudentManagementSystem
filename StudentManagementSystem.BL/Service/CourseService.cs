using StudentManagementSystem.BL.Dtos.CourseDtos;
using StudentManagementSystem.BL.Dtos.InstructorDtos;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;
using StudentManagementSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.BL.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly InstructorRepository _instructorRepository;

        public CourseService(ICourseRepository courseRepository, InstructorRepository instructorRepository)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
        }

        public void CreateCourse(CreateCourseDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                CreditHours = dto.CreditHours,
                InstructorId = dto.InstructorId
            };
            _courseRepository.Add(course);
            _courseRepository.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null) throw new Exception("Course not found");
            _courseRepository.Delete(course);
            _courseRepository.SaveChanges();
        }

        public List<CourseDto> GetAllCourses()
        {
            var list = _courseRepository.GetAllWithInstructor();
            return list.Select(i => new CourseDto
            {
                Id = i.Id,
                Name = i.Name,
                CreditHours = i.CreditHours,
                InstructorId = i.InstructorId ?? 0,
                InstructorName = i.Instructor?.Name ?? string.Empty
            }).ToList();
        }
      
        

        public GetCourseDto GetCourseById(int id)
        {
            var c = _courseRepository.GetByIdWithInstructor(id);
            if (c == null) throw new Exception("Course not found");
            return new GetCourseDto
            {
                
                Name = c.Name,
                CreditHours = c.CreditHours,
                InstructorId = c.InstructorId ?? 0,
                InstructorName = c.Instructor?.Name ?? string.Empty
            };
        }

        public void UpdateCourse(CourseDto dto)
        {
            var course = _courseRepository.GetById(dto.Id);
            if (course == null) throw new Exception("Course not found");
            course.Name = dto.Name;
            course.CreditHours = dto.CreditHours;
            course.InstructorId = dto.InstructorId;
            _courseRepository.Update(course);
            _courseRepository.SaveChanges();
        }
    }
}
