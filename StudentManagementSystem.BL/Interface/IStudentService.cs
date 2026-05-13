using StudentManagementSystem.BL.Dtos.StudentDtos;
using StudentManagementSystem.BL.Dtos.StudentDtos;
using StudentManagementSystem.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Interface
{
    public interface IStudentService
    {
        public StudentManagementSystem.BL.Common.ServiceResponse<object> CreateStudent(CreateStudentDto createStudentDto);
        public StudentManagementSystem.BL.Common.ServiceResponse<StudentManagementSystem.BL.Dtos.StudentDtos.CreateStudentFormDataDto> GetCreateData();
        public List<StudentDto> GetAllStudents();
        public GetStudentDto GetStudentById(int id);
        public StudentManagementSystem.BL.Common.ServiceResponse<object> UpdateStudent(StudentDto studentDto, List<int>? courseIds = null);
        public void DeleteStudent(int id);
    }
}
