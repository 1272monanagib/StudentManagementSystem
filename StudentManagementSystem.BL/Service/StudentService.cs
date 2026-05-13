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

        public StudentManagementSystem.BL.Common.ServiceResponse<StudentManagementSystem.BL.Dtos.StudentDtos.CreateStudentFormDataDto> GetCreateData()
        {
            var response = new StudentManagementSystem.BL.Common.ServiceResponse<StudentManagementSystem.BL.Dtos.StudentDtos.CreateStudentFormDataDto>();
            var data = new StudentManagementSystem.BL.Dtos.StudentDtos.CreateStudentFormDataDto();
            data.Departments = _departmentRepository.GetAll().Select(d => new StudentManagementSystem.BL.Dtos.DepartmentDtos.DepartmentDto { Id = d.Id, Name = d.Name }).ToList();
            data.AcademicYears = _academicYearRepository.GetAll().Select(a => new StudentManagementSystem.BL.Dtos.AcademicYearDtos.AcademicYearDto { Id = a.Id, Name = a.Name }).ToList();
            data.Courses = _courseRepository.GetAll().Select(c => new StudentManagementSystem.BL.Dtos.CourseDtos.CourseDto { Id = c.Id, Name = c.Name, CreditHours = c.CreditHours, InstructorId = c.InstructorId ?? 0 }).ToList();
            response.Data = data;
            return response;
        }
        public ServiceResponse<object> CreateStudent(CreateStudentDto createStudentDto)
        {
            var response = new ServiceResponse<object>();

            // Business validation
            var dept = _departmentRepository.GetById(createStudentDto.DepartmentId);
            if (dept == null)
            {
                response.Success = false;
                response.Errors.Add("Selected department does not exist.");
                return response;
            }

            var ay = _academicYearRepository.GetById(createStudentDto.AcademicYearId);
            if (ay == null)
            {
                response.Success = false;
                response.Errors.Add("Selected academic year does not exist.");
                return response;
            }

            // Validate selected courses
            var courses = new List<Course>();
            if (createStudentDto.CourseIds != null && createStudentDto.CourseIds.Any())
            {
                foreach (var cid in createStudentDto.CourseIds)
                {
                    var course = _courseRepository.GetById(cid);
                    if (course == null)
                    {
                        response.Success = false;
                        response.Errors.Add($"Course with id {cid} not found.");
                        return response;
                    }
                    courses.Add(course);
                }
            }

            // Business rule example: age range
            if (createStudentDto.Age < 15 || createStudentDto.Age > 80)
            {
                response.Success = false;
                response.Errors.Add("Student age must be between 15 and 80.");
                return response;
            }

            // Create student with courses
            var student = new Student
            {
                Name = createStudentDto.Name,
                Email = createStudentDto.Email,
                Age = createStudentDto.Age,
                DepartmentId = createStudentDto.DepartmentId,
                //AcademicYearId = createStudentDto.AcademicYearId
            };

            // Add StudentCourse relations
            foreach (var c in courses)
            {
                student.StudentCourses.Add(new StudentCourse { CourseId = c.Id, Grade = 0 });
            }

            _studerepo.Add(student);
            _studerepo.SaveChanges();

            response.Success = true;
            response.Message = "Student created.";
            return response;
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
                DepartmentId = x.DepartmentId ?? 0,
                DepartmentName = x.Department?.Name ?? string.Empty,
                //AcademicYearId = x.AcademicYearId,
                //AcademicYearName = x.AcademicYear?.Name ?? string.Empty,
                Courses = x.StudentCourses?.Select(sc => sc.Course != null ? $"{sc.Course.Name} ({sc.Course.Instructor?.Name})" : string.Empty).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new List<string>()
            }).ToList();
        }

        public GetStudentDto GetStudentById(int id)
        {
            var student = _studerepo.GetByIdWithDepartment(id);
            if (student == null) throw new Exception();
            return new GetStudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                DepartmentId = student.DepartmentId ?? 0,
                DepartmentName = student.Department?.Name ?? string.Empty,
                //AcademicYearId = student.AcademicYearId,
                //AcademicYearName = student.AcademicYear?.Name ?? string.Empty,
                Courses = student.StudentCourses?.Select(sc => sc.Course != null ? $"{sc.Course.Name} ({sc.Course.Instructor?.Name})" : string.Empty).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new List<string>()
            };
        }

        public ServiceResponse<object> UpdateStudent(StudentDto studentDto, List<int>? courseIds = null)
        {
            var response = new StudentManagementSystem.BL.Common.ServiceResponse<object>();
            var student = _studerepo.GetByIdWithDepartment(studentDto.Id);
            if (student == null)
            {
                response.Success = false;
                response.Errors.Add("Student not found.");
                return response;
            }

            // business validation
            var dept = _departmentRepository.GetById(studentDto.DepartmentId);
            if (dept == null)
            {
                response.Success = false;
                response.Errors.Add("Selected department does not exist.");
                return response;
            }

            student.Name = studentDto.Name;
            student.Age = studentDto.Age;
            student.Email = studentDto.Email;
            student.DepartmentId = studentDto.DepartmentId;
            //student.AcademicYearId = studentDto.AcademicYearId;

            // update courses
            if (courseIds != null)
            {
                // remove existing
                student.StudentCourses.Clear();
                foreach (var cid in courseIds)
                {
                    var c = _courseRepository.GetById(cid);
                    if (c != null)
                        student.StudentCourses.Add(new StudentCourse { CourseId = cid, Grade = 0 });
                }
            }

            _studerepo.Update(student);
            _studerepo.SaveChanges();

            response.Success = true;
            response.Message = "Student updated.";
            return response;
        }
    }
}
