using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetFinalProject.Filters;
using NetFinalProject.Models;
using NetFinalProject.Repository;

namespace NetFinalProject.Controllers
{
    [ServiceFilter(typeof(AuthorizeStudentFilter))]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly UniversityContext _Context;

        public StudentController(IStudentRepository studentRepo, IDepartmentRepository departmentRepo, UniversityContext Context)
        {
            _studentRepo = studentRepo;
            _departmentRepo = departmentRepo; // 👈 مهم جداً
           _Context = Context;
        }
        public async Task<IActionResult> students()
        {
            var students = await _studentRepo.GetAllAsync();
            return View(students);
        }
        // Index + Search
        // StudentController (استخدمي _Context كما عندك)
        public async Task<IActionResult> Index(string? searchTerm, int? deptId)
        {
            var query = _Context.Students
                .Include(s => s.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var t = searchTerm.Trim().ToLower();
                query = query.Where(s =>
                    s.Name.ToLower().StartsWith(t) ||
                    (s.Address != null && s.Address.ToLower().StartsWith(t))
                );
            }

            if (deptId.HasValue)
                query = query.Where(s => s.DeptId == deptId.Value);

            var students = await query.AsNoTracking().ToListAsync();

            // اعادة تعبئة الـ dropdown واظهار القيمة في الحقل
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", deptId);
            ViewBag.SearchTerm = searchTerm;

            return View(students);
        }



        // Details
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return View(student);
        }


        // Create
        // GET: Student/Create
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
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                // لو في error نرجع نفس الـ dropdown
                var departments = await _departmentRepo.GetAllAsync();
                ViewBag.DeptId = new SelectList(departments, "Id", "Name");
                return View(student);
            }

            await _studentRepo.AddAsync(student);
            return RedirectToAction(nameof(Index));
        }


        // Edit
        // GET: student/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return NotFound();

            // ملء Dropdown
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", student.DeptId);
              return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                // إعادة ملء Dropdown لو في error
                var departments = await _departmentRepo.GetAllAsync();
                ViewBag.DeptId = new SelectList(departments, "Id", "Name", student.DeptId);
                
                return View(student);
            }

            await _studentRepo.UpdateAsync(student);
            return RedirectToAction(nameof(Index));
        }
        // GET: Student/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _Context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int studentId)
        {
            var student = await _Context.Students.FindAsync(studentId);
            if (student == null)
                return NotFound();

            _Context.Students.Remove(student);
            await _Context.SaveChangesAsync();

            ViewBag.Deleted = true;
            ViewBag.Message = "✔️ Student deleted successfully!";
            return View("Delete");
        }

    }

}
