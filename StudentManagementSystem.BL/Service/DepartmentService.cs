using StudentManagementSystem.BL.Dtos.DepartmentDtos;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
   
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
          
        }
        public void CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var department = new Department
            {
                Name = createDepartmentDto.Name
            };
            _departmentRepository.Add(department);
            _departmentRepository.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                throw new Exception("Department not found");
            }
            _departmentRepository.Delete(department);
            _departmentRepository.SaveChanges();

        }

        public List<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
           
            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
               
            }).ToList();
        }

        public GetDepartmentDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                throw new Exception("Department not found");
            }
            return new GetDepartmentDto
            {
               
                Name = department.Name,
               
            };
        }

        public void UpdateDepartment(DepartmentDto departmentDto)
        {
            var department = _departmentRepository.GetById(departmentDto.Id);
            if (department == null)
            {
                throw new Exception("Department not found");
            }
            department.Name = departmentDto.Name;
            _departmentRepository.Update(department);
            _departmentRepository.SaveChanges();
        }

      
    }
}
