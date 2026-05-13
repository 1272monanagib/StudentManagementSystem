using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.PL.ViewModel.Instructor;
using InstDto = StudentManagementSystem.BL.Dtos.InstructorDtos;

namespace StudentManagementSystem.PL.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;

        public InstructorController(
            IInstructorService instructorService,
            IDepartmentService departmentService)
        {
            _instructorService = instructorService;
            _departmentService = departmentService;
        }

        public IActionResult Index() => RedirectToAction("GetAll");

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _instructorService.GetAllInstructors();

            var vm = list.Select(i => new InstructorVm
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Salary = i.Salary,
                DepartmentId = i.DepartmentId,
                DepartmentName = i.DepartmentName
            }).ToList();

            return View(vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var ins = _instructorService.GetInstructorById(id);

            return View(new GetInstructorVm
            {
             
                Name = ins.Name,
                Email = ins.Email,
                Salary = ins.Salary,
                DepartmentId = ins.DepartmentId,
                DepartmentName = ins.DepartmentName
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var depts = _departmentService.GetAllDepartments();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateInstructorVm vm)
        {
            if (!ModelState.IsValid)
            {
                var depts = _departmentService.GetAllDepartments();
                ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name");
                return View(vm);
            }

            _instructorService.CreateInstructor(new InstDto.CreateInstructorDto
            {
                Name = vm.Name,
                Email = vm.Email,
                Salary = vm.Salary,
                DepartmentId = vm.DepartmentId
            });

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var i = _instructorService.GetInstructorById(id);

            var vm = new InstructorVm
            {
                Id = id,
                Name = i.Name,
                Email = i.Email,
                Salary = i.Salary,
                DepartmentId = i.DepartmentId,
                DepartmentName = i.DepartmentName
            };

            var depts = _departmentService.GetAllDepartments();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name", vm.DepartmentId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InstructorVm vm)
        {
            if (!ModelState.IsValid)
            {
                var depts = _departmentService.GetAllDepartments();
                ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(depts, "Id", "Name", vm.DepartmentId);
                return View(vm);
            }

            _instructorService.UpdateInstructor(new InstDto.InstructorDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Email = vm.Email,
                Salary = vm.Salary,
                DepartmentId = vm.DepartmentId ,
                DepartmentName = vm.DepartmentName

            });

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var i = _instructorService.GetInstructorById(id);

            return View(new InstructorVm
            {
                Id = id,
                Name = i.Name,
                Email = i.Email,
                Salary = i.Salary,
                DepartmentId = i.DepartmentId,
                DepartmentName = i.DepartmentName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _instructorService.DeleteInstructor(id);
            return RedirectToAction("GetAll");
        }
    }
}