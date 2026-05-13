using Microsoft.AspNetCore.Mvc;
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
        public StudentController(IStudentService studentService, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateStudentVm createStudentVm)
        {
            if (!ModelState.IsValid)
            {
                var depts = _departmentService.GetAllDepartments();
                ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name");
                return View(createStudentVm);
            }

            _studentService.CreateStudent(new StudentDto.CreateStudentDto
            {
                Name = createStudentVm.Name,
                Email = createStudentVm.Email,
                Age = createStudentVm.Age,
                DepartmentId = createStudentVm.DepartmentId
            });

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var studentsDto = _studentService.GetAllStudents();
            var depts = _departmentService.GetAllDepartments().ToDictionary(d => d.Id, d => d.Name);
            var studentVm = studentsDto.Select(x => new StudentVm { Id = x.Id, Name = x.Name, Age = x.Age, Email = x.Email, DepartmentId = x.DepartmentId, DepartmentName = depts.ContainsKey(x.DepartmentId) ? depts[x.DepartmentId] : string.Empty }).ToList();
            return View(studentVm );
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var student = _studentService.GetStudentById(id);
            var dept = _departmentService.GetDepartmentById(student.DepartmentId);
            return View(new GetStudentVm { Id = student.Id, Name = student.Name, Email = student.Email, Age = student.Age, DepartmentName = dept?.Name });

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var student = _studentService.GetStudentById(id);
            var vm = new UpdateStudentVm { Id = student.Id, Name = student.Name, Email = student.Email, Age = student.Age, DepartmentId = student.DepartmentId };
            var depts = _departmentService.GetAllDepartments();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name", vm.DepartmentId);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateStudentVm updateStudentVm)
        {
            if (!ModelState.IsValid)
            {
                var depts = _departmentService.GetAllDepartments();
                ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name", updateStudentVm.DepartmentId);
                return View(updateStudentVm);
            }

            _studentService.UpdateStudent(new StudentDto.StudentDto { Id = updateStudentVm.Id, Name = updateStudentVm.Name, Email = updateStudentVm.Email, Age = updateStudentVm.Age, DepartmentId = updateStudentVm.DepartmentId });
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetStudentById(id);
            var vm = new GetStudentVm { Id = student.Id, Name = student.Name, Email = student.Email, Age = student.Age, DepartmentName = _departmentService.GetDepartmentById(student.DepartmentId)?.Name };
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
