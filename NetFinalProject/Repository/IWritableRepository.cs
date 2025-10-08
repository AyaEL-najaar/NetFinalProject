namespace NetFinalProject.Repository
{
    public interface IWritableRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
