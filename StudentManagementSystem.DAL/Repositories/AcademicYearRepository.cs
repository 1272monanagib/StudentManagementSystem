using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;

namespace StudentManagementSystem.DAL.Repositories
{
    public class AcademicYearRepository : GenericRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(AppDbContext context) : base(context)
        {
        }
    }
}
