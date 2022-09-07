using Microsoft.EntityFrameworkCore;
using Prodavalnik.Areas.Identity.Data;
using Prodavalnik.Core.IReposotories;

namespace Prodavalnik.Core.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext _context, ILogger _logger) 
            : base(_context, _logger)
        {
        }
       public override async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            try
            {
                return await base.GetAll();
            }
            catch (Exception ex)
            {

                this._logger.LogError(ex, "{Repo} GetAll method error", typeof(UserRepository));
                return new List<ApplicationUser>();
            }
        }
        public override async Task<bool> Upsert(ApplicationUser entity)
        {
           
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingUser == null)
                {
                    return await Add(entity);
                }
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(string id)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (existingUser!=null)
                {
                    dbSet.Remove(existingUser);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                this._logger.LogError(ex, "{Repo} Delete method error", typeof(UserRepository));
                return false;
            }
        } 
    }
}
