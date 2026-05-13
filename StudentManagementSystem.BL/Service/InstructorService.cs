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
    public class InstructorService : IInstructorService
    {
        private readonly InstructorRepository _instructorRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public InstructorService(InstructorRepository instructorRepository, IDepartmentRepository departmentRepository)
        {
            _instructorRepository = instructorRepository;
            _departmentRepository = departmentRepository;
        }
        public void CreateInstructor(CreateInstructorDto dto)
        {
            var dept = _departmentRepository.GetById(dto.DepartmentId);
            if (dept == null) throw new Exception("Department not found");
            var ins = new Instructor { Name = dto.Name, Email = dto.Email, Salary = dto.Salary, DepartmentId = dto.DepartmentId };
            _instructorRepository.Add(ins);
            _instructorRepository.SaveChanges();
        }

        public void DeleteInstructor(int id)
        {
            var ins = _instructorRepository.GetById(id);
            if (ins == null) throw new Exception("Instructor not found");
            _instructorRepository.Delete(ins);
            _instructorRepository.SaveChanges();
        }

        public List<InstructorDto> GetAllInstructors()
        {
            var list = _instructorRepository.GetAllWithDepartment();
            return list.Select(i => new InstructorDto { Id = i.Id, Name = i.Name, Email = i.Email, Salary = i.Salary, 
                DepartmentId = i.DepartmentId ?? 0, DepartmentName = i.Department?.Name ?? string.Empty }).ToList();
        }

        public GetInstructorDto GetInstructorById(int id)
        {
            var ins = _instructorRepository.GetByIdWithDepartment(id);
            if (ins == null) throw new Exception("Instructor not found");
            return new GetInstructorDto { Name = ins.Name, Email = ins.Email, Salary = ins.Salary, 
                DepartmentId = ins.DepartmentId ?? 0, DepartmentName = ins.Department?.Name ?? string.Empty };
        }

        public void UpdateInstructor(InstructorDto dto)
        {
            var ins = _instructorRepository.GetById(dto.Id);
            if (ins == null) throw new Exception("Instructor not found");
            ins.Name = dto.Name;
            ins.Email = dto.Email;
            ins.Salary = dto.Salary;
            ins.DepartmentId = dto.DepartmentId;
            _instructorRepository.Update(ins);
            _instructorRepository.SaveChanges();
        }

       
    }
}
