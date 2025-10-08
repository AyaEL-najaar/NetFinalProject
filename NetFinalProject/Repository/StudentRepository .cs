using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext context) => _context = context;

        public async Task<Student> AddAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Student>> GetAllAsync() =>
            await _context.Students.AsNoTracking().ToListAsync();

        public async Task<Student?> GetByIdAsync(int id) =>
            await _context.Students.FindAsync(id);

        public async Task<Student?> UpdateAsync(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Student>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            return await _context.Students
                .Where(e => e.Name.Contains(keyword))
                .AsNoTracking()
                .ToListAsync();
        }


    }

}
