using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BL.Interface;
using StudentManagementSystem.PL.ViewModel.Course;
using CourseDto = StudentManagementSystem.BL.Dtos.CourseDtos;

namespace StudentManagementSystem.PL.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public CourseController(ICourseService courseService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _courseService.GetAllCourses();
            var vm = courses.Select(c => new CourseVm { Id = c.Id, Name = c.Name, CreditHours = c.CreditHours, InstructorId = c.InstructorId, InstructorName = c.InstructorName }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var c = _courseService.GetCourseById(id);
            return View(new GetCourseVm { Name = c.Name, CreditHours = c.CreditHours, InstructorId = c.InstructorId, InstructorName = c.InstructorName });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var instructors = _instructorService.GetAllInstructors();
            ViewBag.Instructors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(instructors, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCourseVm vm)
        {
            if (!ModelState.IsValid)
            {
                var instructors = _instructorService.GetAllInstructors();
                ViewBag.Instructors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(instructors, "Id", "Name");
                return View(vm);
            }

            _courseService.CreateCourse(new CourseDto.CreateCourseDto { Name = vm.Name, CreditHours = vm.CreditHours, 
                InstructorId = vm.InstructorId });
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var c = _courseService.GetCourseById(id);
            var vm = new CreateCourseVm { Name = c.Name, CreditHours = c.CreditHours, InstructorId = c.InstructorId };
            var instructors = _instructorService.GetAllInstructors();
            ViewBag.Instructors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(instructors, "Id", "Name", vm.InstructorId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateCourseVm vm)
        {
            if (!ModelState.IsValid)
            {
                var instructors = _instructorService.GetAllInstructors();
                ViewBag.Instructors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(instructors, "Id", "Name", vm.InstructorId);
                return View(vm);
            }

            _courseService.UpdateCourse(new CourseDto.CourseDto { Id = id, Name = vm.Name, CreditHours = vm.CreditHours, InstructorId = vm.InstructorId });
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var c = _courseService.GetCourseById(id);
            return View(new CourseVm { Id = id, Name = c.Name, CreditHours = c.CreditHours, InstructorId = c.InstructorId, InstructorName = c.InstructorName });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.DeleteCourse(id);
            return RedirectToAction("GetAll");
        }
    }
}
