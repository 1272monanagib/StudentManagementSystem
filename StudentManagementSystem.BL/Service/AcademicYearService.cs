using StudentManagementSystem.BL.Dtos.AcademicYearDtos;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.BL.Service
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        public AcademicYearService(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public void CreateAcademicYear(CreateAcademicYearDto dto)
        {
            var ay = new AcademicYear { Name = dto.Name };
            _academicYearRepository.Add(ay);
            _academicYearRepository.SaveChanges();
        }

        public void DeleteAcademicYear(int id)
        {
            var ay = _academicYearRepository.GetById(id);
            if (ay == null) throw new Exception("Academic year not found");
            _academicYearRepository.Delete(ay);
            _academicYearRepository.SaveChanges();
        }

        public List<AcademicYearDto> GetAllAcademicYears()
        {
            var list = _academicYearRepository.GetAll();
            return list.Select(a => new AcademicYearDto { Id = a.Id, Name = a.Name }).ToList();
        }

        public GetAcademicYearDto GetAcademicYearById(int id)
        {
            var ay = _academicYearRepository.GetById(id);
            if (ay == null) throw new Exception("Academic year not found");
            return new GetAcademicYearDto { Name = ay.Name };
        }

        public void UpdateAcademicYear(AcademicYearDto dto)
        {
            var ay = _academicYearRepository.GetById(dto.Id);
            if (ay == null) throw new Exception("Academic year not found");
            ay.Name = dto.Name;
            _academicYearRepository.Update(ay);
            _academicYearRepository.SaveChanges();
        }
    }
}
