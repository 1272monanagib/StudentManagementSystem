using StudentManagementSystem.BL.Dtos.StudentDto;
using StudentManagementSystem.BL.Dtos.StudentDtos;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.BL.Common;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studerepo;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentService(IStudentRepository studerepo, IDepartmentRepository departmentRepository, IAcademicYearRepository academicYearRepository, ICourseRepository courseRepository)
        {
            _studerepo = studerepo;
            _departmentRepository = departmentRepository;
            _academicYearRepository = academicYearRepository;
            _courseRepository = courseRepository;
        }

    
        public void CreateStudent(CreateStudentDto createStudentDto)
        {

            var student = new Student
            {
                Name = createStudentDto.Name,
                Email = createStudentDto.Email,
                Age = createStudentDto.Age,
                DepartmentId = createStudentDto.DepartmentId,
                AcademicYearId = createStudentDto.AcademicYearId,
                StudentCourses = new List<StudentCourse>()
            };
            foreach (var courseId in createStudentDto.CourseIds)
            {
                student.StudentCourses.Add(new StudentCourse { CourseId = courseId});
            }

            _studerepo.Add(student);
            _studerepo.SaveChanges();

        
        }

        public void DeleteStudent(int id)
        {
            var student = _studerepo.GetById(id);
            if (student == null)
                throw new Exception();
            _studerepo.Delete(student);
            _studerepo.SaveChanges();
            
        }

        public List<StudentDto> GetAllStudents()
        {
            var students = _studerepo.GetAllWithDepartment();

            return students.Select(x => new StudentDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Age = x.Age,
                DepartmentName = x.Department?.Name ?? string.Empty,
                AcademicYearName = x.AcademicYear?.Name ?? string.Empty,
                CoursesNames = x.StudentCourses?
    .Select(sc => sc.Course != null
        ? $"{sc.Course.Name} ({sc.Course.Instructor?.Name})"
        : string.Empty)
    .Where(s => !string.IsNullOrWhiteSpace(s))
    .ToList() ?? new List<string>()
            }).ToList();
        }
        public GetStudentDto GetStudentById(int id)
        {
            var student = _studerepo.GetByIdWithDepartment(id);

            if (student == null)
                throw new Exception("Student not found");

            return new GetStudentDto
            {
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                DepartmentName = student.Department?.Name ?? string.Empty,
                AcademicYearName = student.AcademicYear?.Name ?? string.Empty,

                CourseIds = student.StudentCourses
              .Select(sc => sc.CourseId)
              .ToList()
            };
        }
        public void UpdateStudent(UpdateStudentDto updateStudentDto)
        {
           
            var student = _studerepo.GetById(updateStudentDto.Id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

     

            student.Name = updateStudentDto.Name;
            student.Age = updateStudentDto.Age;
            student.Email = updateStudentDto.Email;
            student.DepartmentId = updateStudentDto.DepartmentId;
            student.AcademicYearId = updateStudentDto.AcademicYearId;
          

            _studerepo.Update(student);
            _studerepo.SaveChanges();

        }
    }
}
