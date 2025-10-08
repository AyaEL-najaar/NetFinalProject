using Microsoft.AspNetCore.Mvc;
using NetFinalProject.Filters;
using NetFinalProject.Models;
using NetFinalProject.Repository;

namespace NetFinalProject.Controllers
{
    [ServiceFilter(typeof(AuthorizeStudentFilter))]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepo;

        public StudentController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public async Task<IActionResult> students()
        {
            var students = await _studentRepo.GetAllAsync();
            return View(students);
        }
        // Index + Search
        public async Task<IActionResult> Index(string? search)
        {
            var students = string.IsNullOrEmpty(search)
                ? await _studentRepo.GetAllAsync()
                : await _studentRepo.SearchAsync(search);

            ViewBag.Search = search;
            return View(students);
        }

        // Details
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        // Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            await _studentRepo.AddAsync(student);
            return RedirectToAction(nameof(Index));
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            await _studentRepo.UpdateAsync(student);
            return RedirectToAction(nameof(Index));
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
