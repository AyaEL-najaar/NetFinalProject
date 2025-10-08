namespace NetFinalProject.Repository
{
    public interface IReadableRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
    }
}
