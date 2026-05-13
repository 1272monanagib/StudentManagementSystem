using StudentManagementSystem.BL.Dtos.AcademicYearDtos;
using System.Collections.Generic;

namespace StudentManagementSystem.BL.Interface
{
    public interface IAcademicYearService
    {
        void CreateAcademicYear(CreateAcademicYearDto dto);
        List<AcademicYearDto> GetAllAcademicYears();
        GetAcademicYearDto GetAcademicYearById(int id);
        void UpdateAcademicYear(AcademicYearDto dto);
        void DeleteAcademicYear(int id);
    }
}
