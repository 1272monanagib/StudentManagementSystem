using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Student> GetAllWithDepartment()
        {
            return _context.Students
                .Include(s => s.Department)
                //.Include(s => s.AcademicYear)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                        .ThenInclude(c => c.Instructor)
                .ToList();
        }

        public Student? GetByIdWithDepartment(int id)
        {
            return _context.Students
                .Include(s => s.Department)
                //.Include(s => s.AcademicYear)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                        .ThenInclude(c => c.Instructor)
                .FirstOrDefault(s => s.Id == id);
        }
    }
}
