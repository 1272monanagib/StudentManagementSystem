using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.BL.Service;
using DeptDto = StudentManagementSystem.BL.Dtos.DepartmentDtos;
using StudentManagementSystem.PL.ViewModel.Department;
using StudentManagementSystem.PL.ViewModel.Student;

namespace StudentManagementSystem.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentVm createDepartmentVm)
        {
            _departmentService.CreateDepartment(new DeptDto.CreateDepartmentDto
            {
                Name = createDepartmentVm.Name
            });
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departmentsDto = _departmentService.GetAllDepartments();
            var departmentVm = departmentsDto.Select(x => new DepartmentVm { Id = x.Id, Name = x.Name}).ToList();
            return View(departmentVm);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
            return View(new GetDepartmentVm { Name = department.Name });

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
            return View(new DepartmentVm { Id = id, Name = department.Name });
        }
        [HttpPost]
        public IActionResult Update(DepartmentVm departmentVm)
        {
            _departmentService.UpdateDepartment(new DeptDto.DepartmentDto { Id = departmentVm.Id, Name = departmentVm.Name });
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
            return View(new DepartmentVm { Id = id, Name = department.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _departmentService.DeleteDepartment(id);
            return RedirectToAction("GetAll");
        }
    }
}
