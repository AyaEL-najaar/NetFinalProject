using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public interface IDepartmentRepository : IReadableRepository<Department>, IWritableRepository<Department>
    {
        Task<Department?> GetByIdAsync(int id); // مثال فلترة حسب الاسم
        Task<IEnumerable<Department?>> SearchAsync(string KeyWord);
    }

}
