using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public interface IDepartmentRepository : IReadableRepository<Department>, IWritableRepository<Department>
    {
        Task<IEnumerable<Department?>> GetAllAsync();

        Task<Department?> GetByIdAsync(int id);
        Task<IEnumerable<Department?>> SearchAsync(string KeyWord);
    }

}
