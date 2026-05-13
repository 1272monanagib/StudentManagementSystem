using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;

namespace StudentManagementSystem.DAL.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Course> GetAllWithInstructor()
        {
            return _context.Courses.Include(i => i.Instructor).Include(i => i.StudentCourses).ToList();
        }

        public Course? GetByIdWithInstructor(int id)
        {
            return _context.Courses.Include(i => i.Instructor).Include(i => i.StudentCourses).FirstOrDefault(c => c.Id == id);
        }
    }
}
