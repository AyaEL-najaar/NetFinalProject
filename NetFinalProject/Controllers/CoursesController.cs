using Microsoft.AspNetCore.Mvc;
using NetFinalProject.Filters;
using NetFinalProject.Models;
using NetFinalProject.Repository;


namespace NetFinalProject.Controllers
{
    [ServiceFilter(typeof(AuthorizeStudentFilter))]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;

        // Constructor
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }
        public IActionResult Courses()
        {
            var courses = _courseRepo.GetAllAsync();
            return View(courses);
        }
        // GET: Course
        public async Task<IActionResult> Index(string? search)
        {
            var students = string.IsNullOrEmpty(search)
                ? await _courseRepo.GetAllAsync()
                : await _courseRepo.SearchAsync(search);

            ViewBag.Search = search;
            return View(students);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid) return View(course);

            await _courseRepo.AddAsync(course);
            return RedirectToAction(nameof(Index));
        }

        // GET: Course/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if (!ModelState.IsValid) return View(course);

            await _courseRepo.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
        }

        // GET: Course/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
              


        //Join Courses
        public IActionResult JoinCourse(int courseId)
        {
            HttpContext.Session.SetInt32("SelectedCourseId", courseId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> JoinedCourses()
        {
            var courses = await _courseRepo.GetAllAsync();
            var selectedId = HttpContext.Session.GetInt32("SelectedCourseId");

            ViewBag.SelectedCourseId = selectedId;
            return View(courses);
        }

    }

}
