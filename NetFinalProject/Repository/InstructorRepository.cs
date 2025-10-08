using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly UniversityContext _context;

        public InstructorRepository(UniversityContext context) => _context = context;

        public async Task<Instructor> AddAsync(Instructor entity)
        {
            await _context.Instructors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null) return false;
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync() =>
            await _context.Instructors
                .Include(i => i.Department)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Instructor?> GetByIdAsync(int id) =>
            await _context.Instructors
                .Include(i => i.Department)
                .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Instructor?> UpdateAsync(Instructor entity)
        {
            _context.Instructors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<Instructor>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            return await _context.Instructors
                .Where(i => i.Name.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Instructor>> GetByDepartmentAsync(int deptId) =>
            await _context.Instructors
                .Where(i => i.DeptId == deptId)
                .Include(i => i.Department)
                .ToListAsync();
    }

}
