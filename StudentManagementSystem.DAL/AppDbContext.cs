using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAL.Entites;

namespace StudentManagementSystem.DAL
{
    public class AppDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Department.Name required
            modelBuilder.Entity<Department>(b =>
            {
                b.HasKey(d => d.Id);
                b.Property(d => d.Name).IsRequired().HasMaxLength(200);
                b.HasMany(d => d.Students).WithOne(s => s.Department).HasForeignKey(s => s.DepartmentId).OnDelete(DeleteBehavior.Cascade);
            });

            // Student configuration
            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(s => s.Id);
                b.Property(s => s.Name).IsRequired().HasMaxLength(200);
                b.Property(s => s.Email).HasMaxLength(200);
            });

            // Seed departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "HR" },
                new Department { Id = 3, Name = "Finance" }
            );

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=StudentManagementSystemDB;Trusted_Connection=True;Encrypt=False");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
