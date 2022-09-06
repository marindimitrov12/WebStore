namespace Prodavalnik.Core.IReposotories
{
    public interface IGenericRepository<T>where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Delete(string id);

        Task<bool> Upsert(T entity);

    }
}
