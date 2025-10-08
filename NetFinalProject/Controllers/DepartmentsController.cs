using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetFinalProject.Filters;
using NetFinalProject.Models;
using NetFinalProject.Repository;

namespace NetFinalProject.Controllers
{
    [ServiceFilter(typeof(AuthorizeStudentFilter))]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _deptRepo;
        private readonly UniversityContext _context;
        public DepartmentController(IDepartmentRepository deptRepo, UniversityContext context)
        {
            _deptRepo = deptRepo;
            _context = context;
        }
        public IActionResult Departments()
        {
            var courses = _deptRepo.GetAllAsync();
            return View(Departments);
        }
        public async Task<IActionResult> Index(string? search)
        {
            var depts = string.IsNullOrEmpty(search)
                ? await _deptRepo.GetAllAsync()
                : await _deptRepo.SearchAsync(search);

            ViewBag.Search = search;
            return View(depts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dept = await _deptRepo.GetByIdAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            if (!ModelState.IsValid) return View(dept);
            await _deptRepo.AddAsync(dept);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _deptRepo.GetByIdAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (!ModelState.IsValid) return View(dept);
            await _deptRepo.UpdateAsync(dept);
            return RedirectToAction(nameof(Index));
        }

        // GET: Department/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // POST: Department/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            ViewBag.Deleted = true;
            ViewBag.Message = "✔️ Department deleted successfully!";
            return View("Delete");
        }

    }

}
