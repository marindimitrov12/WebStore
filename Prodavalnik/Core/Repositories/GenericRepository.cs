using Microsoft.EntityFrameworkCore;
using Prodavalnik.Areas.Identity.Data;
using Prodavalnik.Core.IReposotories;

namespace Prodavalnik.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(
            ApplicationDbContext context,
            ILogger logger)
        {
            _context= context;
            _logger= logger;
            dbSet = context.Set<T>();
        }
        public async Task<int> Count()
        {
            return await dbSet.CountAsync();
        }
        public  async Task<IQueryable<T>> Skip(int i)
        {
            return  dbSet.Skip(i);
        }
        public async Task<IEnumerable<T>> Take(int i)
        {
            return dbSet.Take(i);
        }
        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
