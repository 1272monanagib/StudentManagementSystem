using StudentManagementSystem.BL.Dtos.DepartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL.Interface
{
    public interface IDepartmentService
    {
        public void CreateDepartment(CreateDepartmentDto createDepartmentDto);
        public List<DepartmentDto> GetAllDepartments();
        public GetDepartmentDto GetDepartmentById(int id);
        public void UpdateDepartment(DepartmentDto departmentDto);
        public void DeleteDepartment(int id);
    }
}
