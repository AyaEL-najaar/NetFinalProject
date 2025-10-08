using NetFinalProject.Models;

namespace NetFinalProject.Repository
{
    public interface IStudentRepository : IReadableRepository<Student>, IWritableRepository<Student>
    {
        Task<Student?> GetByIdAsync(int StudentId);
        Task<IEnumerable<Student?>> SearchAsync(string keyword);
    }
}
