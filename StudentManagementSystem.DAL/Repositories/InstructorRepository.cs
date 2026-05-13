using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.DAL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.DAL.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, Interface.IInstructorRepository
    {
        private readonly AppDbContext _context;
        public InstructorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Instructor> GetAllWithDepartment()
        {
            return _context.Instructors.Include(i => i.Courses).Include(i => i.Department).ToList();
        }

        public Instructor? GetByIdWithDepartment(int id)
        {
            return _context.Instructors.Include(i => i.Courses).Include(i => i.Department).FirstOrDefault(i => i.Id == id);
        }
    }
}
