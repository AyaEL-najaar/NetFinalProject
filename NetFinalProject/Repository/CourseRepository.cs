using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityContext _context;

        public CourseRepository(UniversityContext context) => _context = context;

        public async Task<Course> AddAsync(Course entity)
        {
            await _context.Courses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Course>> GetAllAsync() =>
            await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Course?> GetByIdAsync(int id) =>
            await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Course?> UpdateAsync(Course entity)
        {
            _context.Courses.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<Course>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            return await _context.Courses
                .Include(c => c.Department)
                .Where(c => c.Name.Contains(keyword))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByDepartmentAsync(int deptId) =>
            await _context.Courses
                .Where(c => c.DeptId == deptId)
                .Include(c => c.Department)
                .ToListAsync();
    }

}
