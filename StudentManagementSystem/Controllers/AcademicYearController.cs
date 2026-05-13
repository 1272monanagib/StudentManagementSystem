using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.PL.ViewModel.AcademicYear;
using AYDto = StudentManagementSystem.BL.Dtos.AcademicYearDtos;

namespace StudentManagementSystem.PL.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly IAcademicYearService _academicYearService;
        public AcademicYearController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        public IActionResult Index() => RedirectToAction("GetAll");

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _academicYearService.GetAllAcademicYears();
            var vm = list.Select(a => new AcademicYearVm { Id = a.Id, Name = a.Name }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var a = _academicYearService.GetAcademicYearById(id);
            return View(new GetAcademicYearVm { Name = a.Name });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAcademicYearVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _academicYearService.CreateAcademicYear(new AYDto.CreateAcademicYearDto { Name = vm.Name });
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var a = _academicYearService.GetAcademicYearById(id);
            return View(new AcademicYearVm { Id = id, Name = a.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AcademicYearVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _academicYearService.UpdateAcademicYear(new AYDto.AcademicYearDto { Id = vm.Id, Name = vm.Name });
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var a = _academicYearService.GetAcademicYearById(id);
            return View(new AcademicYearVm { Id = id, Name = a.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _academicYearService.DeleteAcademicYear(id);
            return RedirectToAction("GetAll");
        }
    }
}
