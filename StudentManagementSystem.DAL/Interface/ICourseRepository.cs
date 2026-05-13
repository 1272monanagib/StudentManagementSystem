using StudentManagementSystem.DAL.Entites;
using System.Collections.Generic;

namespace StudentManagementSystem.DAL.Interface
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        List<Course> GetAllWithInstructor();
        Course? GetByIdWithInstructor(int id);
    }
}
