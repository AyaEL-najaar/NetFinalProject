using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly UniversityContext _context;
        public DepartmentRepository(UniversityContext context) => _context = context;

        public async Task<Department> AddAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Department>> GetAllAsync() =>
        await _context.Departments.ToListAsync();

        public async Task<Department?> GetByIdAsync(int id) =>
            await _context.Departments.FindAsync(id);

        public async Task<Department?> UpdateAsync(Department entity)
        {
            _context.Departments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Department>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            return await _context.Departments
                .Where(d => d.Name.Contains(keyword))
                .ToListAsync();
        }

    }

}
