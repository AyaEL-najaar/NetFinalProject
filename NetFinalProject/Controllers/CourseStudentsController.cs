using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject.Controllers
{
    public class CourseStudentsController : Controller
    {
        private readonly UniversityContext _context;

        public CourseStudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: CourseStudents
        public async Task<IActionResult> Index()
        {
            var universityContext = _context.CourseStudents.Include(c => c.Course).Include(c => c.Student);
            return View(await universityContext.ToListAsync());
        }

        // GET: CourseStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudents = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (courseStudents == null)
            {
                return NotFound();
            }

            return View(courseStudents);
        }

        // GET: CourseStudents/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name");
            return View();
        }

        // POST: CourseStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,StudentId")] CourseStudents courseStudents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseStudents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseStudents.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", courseStudents.StudentId);
            return View(courseStudents);
        }

        // GET: CourseStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudents = await _context.CourseStudents.FindAsync(id);
            if (courseStudents == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseStudents.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", courseStudents.StudentId);
            return View(courseStudents);
        }

        // POST: CourseStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,StudentId")] CourseStudents courseStudents)
        {
            if (id != courseStudents.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseStudents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseStudentsExists(courseStudents.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseStudents.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", courseStudents.StudentId);
            return View(courseStudents);
        }

        // GET: CourseStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudents = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (courseStudents == null)
            {
                return NotFound();
            }

            return View(courseStudents);
        }

        // POST: CourseStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseStudents = await _context.CourseStudents.FindAsync(id);
            if (courseStudents != null)
            {
                _context.CourseStudents.Remove(courseStudents);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseStudentsExists(int id)
        {
            return _context.CourseStudents.Any(e => e.CourseId == id);
        }
    }
}
