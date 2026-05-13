using StudentManagementSystem.BL.Dtos.InstructorDtos;

using System.Collections.Generic;

namespace StudentManagementSystem.BL.Interface
{
    public interface IInstructorService
    {
        void CreateInstructor(CreateInstructorDto dto);
        List<InstructorDto> GetAllInstructors();
        GetInstructorDto GetInstructorById(int id);
        void UpdateInstructor(InstructorDto dto);
        void DeleteInstructor(int id);
    }
}
