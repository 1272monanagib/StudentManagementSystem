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
        public void CreateStudent(CreateStudentDto createStudentDto);
        public List<StudentDto> GetAllStudents();
        public GetStudentDto GetStudentById(int id);
        public void UpdateStudent(UpdateStudentDto updateStudentDto);
        public void DeleteStudent(int id);
    }
}
