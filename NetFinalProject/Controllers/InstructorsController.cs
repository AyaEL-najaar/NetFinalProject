using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetFinalProject.Filters;
using NetFinalProject.Models;
using NetFinalProject.Repository;
using System;

namespace NetFinalProject.Controllers
{
    [ServiceFilter(typeof(AuthorizeStudentFilter))]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instrRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly UniversityContext _context;
        public InstructorController(IInstructorRepository instrRepo, IDepartmentRepository departmentRepo, UniversityContext context)
        {
            _instrRepo = instrRepo;
            _departmentRepo = departmentRepo;
            _context = context;
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
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.DeptId = new SelectList(departments, "Id", "Name");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instr)
        {
            if (!ModelState.IsValid)
            {
                // لو في error نرجع نفس الـ dropdown
                var departments = await _departmentRepo.GetAllAsync();
                ViewBag.DeptId = new SelectList(departments, "Id", "Name");
                return View(instr);
            }

            await _instrRepo.AddAsync(instr);
            return RedirectToAction(nameof(Index));
        }
        // GET: student/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instr = await _instrRepo.GetByIdAsync(id);
            if (instr == null) return NotFound();

            // ملء Dropdown
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", instr.DeptId);
            return View(instr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Instructor instr)
        {
            if (!ModelState.IsValid)
            {
                // إعادة ملء Dropdown لو في error
                var departments = await _departmentRepo.GetAllAsync();
                ViewBag.DeptId = new SelectList(departments, "Id", "Name", instr.DeptId);

                return View(instr);
            }

            await _instrRepo.UpdateAsync(instr);
            return RedirectToAction(nameof(Index));
        }

        // GET: Instructor/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
                return NotFound();

            return View(instructor);
        }

        // POST: Instructor/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
                return NotFound();

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            ViewBag.Deleted = true;
            ViewBag.Message = "✔️ Instructor deleted successfully!";
            return View("Delete");
        }

    }

}
