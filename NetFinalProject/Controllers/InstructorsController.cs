using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly UniversityContext _context;

        public InstructorsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Instructors
                .Include(i => i.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(i => i.Name.Contains(searchTerm));
            }

            ViewBag.SearchTerm = searchTerm;

            return View(await query.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Courses) // لو عايزة تعرض الكورسات اللي بيدرسها
                .FirstOrDefaultAsync(m => m.Id == id);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Instructors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary,Address,Image,DeptId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DeptId);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null) return NotFound();

            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DeptId);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary,Address,Image,DeptId")] Instructor instructor)
        {
            if (id != instructor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instructors.Any(e => e.Id == instructor.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DeptId);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
