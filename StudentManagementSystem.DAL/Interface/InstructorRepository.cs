using StudentManagementSystem.DAL.Entites;

namespace StudentManagementSystem.DAL.Interface
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        List<Instructor> GetAllWithDepartment();
        Instructor? GetByIdWithDepartment(int id);
    }
}
