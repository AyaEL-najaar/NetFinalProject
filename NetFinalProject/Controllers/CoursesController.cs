using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IDepartmentRepository _departmentRepo; // 👈 هنا
        private readonly IInstructorRepository _instructorRepo;
        private readonly UniversityContext _context;
        public CourseController(ICourseRepository courseRepo, IDepartmentRepository departmentRepo, IInstructorRepository instructorRepo, UniversityContext context)
        {
            _courseRepo = courseRepo;
            _departmentRepo = departmentRepo;
            _instructorRepo = instructorRepo;// 👈 هنا
            _context = context;
        }
        public async Task<IActionResult> Courses()
        {
            var courses = await _courseRepo.GetAllAsync();
            return View(courses);
        }

        // GET: Course
        public async Task<IActionResult> Index(string? search)
        {
            var courses = string.IsNullOrEmpty(search)
                ? await _courseRepo.GetAllAsync()
                : await _courseRepo.SearchAsync(search);

            ViewBag.Search = search;
            return View(courses);
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

        // GET: Student/Create
        [HttpGet]
        public async Task<IActionResult> Create()

        {

            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var instructors = await _instructorRepo.GetAllAsync();
            ViewBag.Instructors = new SelectList(instructors, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(await _departmentRepo.GetAllAsync(), "Id", "Name", course.DeptId);
                ViewBag.Instructors = new SelectList(await _instructorRepo.GetAllAsync(), "Id", "Name", course.InstructorId);
                return View(course);
            }

            await _courseRepo.AddAsync(course);
            return RedirectToAction(nameof(Index));
        }


        // GET: Course/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return NotFound();

            // ملء Dropdown
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", course.DeptId);
            var instructors = await _instructorRepo.GetAllAsync();
            ViewBag.Instructors = new SelectList(instructors, "Id", "Name", course.InstructorId);

            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                // إعادة ملء Dropdown لو في error
                var departments = await _departmentRepo.GetAllAsync();
                ViewBag.Departments = new SelectList(departments, "Id", "Name", course.DeptId);
                var instructors = await _instructorRepo.GetAllAsync();
                ViewBag.Instructors = new SelectList(instructors, "Id", "Name", course.InstructorId);
                return View(course);
            }

            await _courseRepo.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
        }


        // GET: Course/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        // POST: Course/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            ViewBag.Deleted = true;
            ViewBag.Message = "✔️ Course deleted successfully!";
            return View("Delete");
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
