using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.BL.Dtos.StudentDtos;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.BL.Service;
using StudentManagementSystem.PL.ViewModel.Department;
using StudentManagementSystem.PL.ViewModel.Student;
using StudentDto = StudentManagementSystem.BL.Dtos.StudentDtos;

namespace StudentManagementSystem.PL.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IAcademicYearService _academicYearService;
        private readonly ICourseService _courseService;
        public StudentController(IStudentService studentService, IDepartmentService departmentService, IAcademicYearService academicYearService, ICourseService courseService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _academicYearService = academicYearService;
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var depts = _departmentService.GetAllDepartments();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name");
            ViewBag.AcademicYears = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_academicYearService.GetAllAcademicYears(), "Id", "Name");
            ViewBag.Courses = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_courseService.GetAllCourses(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateStudentVm createStudentVm)
        {
            if (!ModelState.IsValid)
            {
                return View(createStudentVm);
            }

            _studentService.CreateStudent(new StudentDto.CreateStudentDto
            {
                Name = createStudentVm.Name,
                Email = createStudentVm.Email,
                Age = createStudentVm.Age,
                DepartmentId = createStudentVm.DepartmentId,
                AcademicYearId = createStudentVm.AcademicYearId,
                CourseIds = createStudentVm.SelectedCourseIds

            });

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list= _studentService.GetAllStudents();
            var vm = list.Select(x => new StudentVm { Id = x.Id, Name = x.Name, Age = x.Age, 
                Email = x.Email, DepartmentName = x.DepartmentName , 
                AcademicYearName = x.AcademicYearName , CoursesNames = string.Join(", ", x.CoursesNames) }).ToList();
          
            return View(vm);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var student = _studentService.GetStudentById(id);
            
            return View(new GetStudentVm { Name = student.Name, Email = student.Email, Age = student.Age, 
                DepartmentName = student.DepartmentName ,  AcademicYearName = student.AcademicYearName , 
                CoursesNames = string.Join(", ", student.CourseIds) });

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var student = _studentService.GetStudentById(id);

            var vm = new UpdateStudentVm
            {
                Id = id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,

                DepartmentId = student.DepartmentId ,
                AcademicYearId = student.AcademicYearId ,

                SelectedCourseIds = student.CourseIds.ToList()
            };

            ViewBag.Departments = new SelectList(_departmentService.GetAllDepartments(), "Id", "Name", vm.DepartmentId);
            ViewBag.AcademicYears = new SelectList(_academicYearService.GetAllAcademicYears(), "Id", "Name", vm.AcademicYearId);
            ViewBag.Courses = new MultiSelectList(_courseService.GetAllCourses(), "Id", "Name");

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateStudentVm vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_departmentService.GetAllDepartments(), "Id", "Name");
                ViewBag.AcademicYears = new SelectList(_academicYearService.GetAllAcademicYears(), "Id", "Name");
                ViewBag.Courses = new MultiSelectList(_courseService.GetAllCourses(), "Id", "Name");

                return View(vm);
            }

            _studentService.UpdateStudent(new UpdateStudentDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Email = vm.Email,
                Age = vm.Age,
                DepartmentId = vm.DepartmentId,
                AcademicYearId = vm.AcademicYearId,
                CourseIds = vm.SelectedCourseIds  
            });

            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetStudentById(id);
            var vm = new StudentVm { Id = id, Name = student.Name, Email = student.Email, Age = student.Age,
            DepartmentName= student.DepartmentName ,
            AcademicYearName = student.AcademicYearName , CoursesNames = string.Join(", ", student.CourseIds) };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.DeleteStudent(id);
            return RedirectToAction("GetAll");
        }
    }
}
