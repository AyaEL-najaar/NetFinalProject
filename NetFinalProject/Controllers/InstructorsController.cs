using Microsoft.AspNetCore.Mvc;
using NetFinalProject.Filters;
using NetFinalProject.Models;
using NetFinalProject.Repository;

namespace NetFinalProject.Controllers
{
    [ServiceFilter(typeof(AuthorizeStudentFilter))]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instrRepo;

        public InstructorController(IInstructorRepository instrRepo)
        {
            _instrRepo = instrRepo;
        }
        public IActionResult Instructors()
        {
            var courses = _instrRepo.GetAllAsync();
            return View(Instructors);
        }
        public async Task<IActionResult> Index(string? search)
        {
            var instructors = string.IsNullOrEmpty(search)
                ? await _instrRepo.GetAllAsync()
                : await _instrRepo.SearchAsync(search);

            ViewBag.Search = search;
            return View(instructors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instrRepo.GetByIdAsync(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Instructor instr)
        {
            if (!ModelState.IsValid) return View(instr);
            await _instrRepo.AddAsync(instr);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instr = await _instrRepo.GetByIdAsync(id);
            if (instr == null) return NotFound();
            return View(instr);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Instructor instr)
        {
            if (!ModelState.IsValid) return View(instr);
            await _instrRepo.UpdateAsync(instr);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _instrRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
