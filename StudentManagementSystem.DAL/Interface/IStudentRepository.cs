using StudentManagementSystem.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL.Interface
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        List<Student> GetAllWithDepartment();
        Student? GetByIdWithDepartment(int id);
    }
}
