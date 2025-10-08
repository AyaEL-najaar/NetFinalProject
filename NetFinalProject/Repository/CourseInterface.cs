using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public interface ICourseRepository : IReadableRepository<Course>, IWritableRepository<Course>
    {
        Task<IEnumerable<Course>> GetByDepartmentAsync(int deptId);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<IEnumerable<Course>> SearchAsync(string keyword);

    }

}
