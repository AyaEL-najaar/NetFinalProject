using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public interface IInstructorRepository : IReadableRepository<Instructor>, IWritableRepository<Instructor>
    {
        Task<IEnumerable<Instructor>> GetByDepartmentAsync(int deptId);
        Task<IEnumerable<Instructor>> SearchAsync(string KeyWord);
    }

}
