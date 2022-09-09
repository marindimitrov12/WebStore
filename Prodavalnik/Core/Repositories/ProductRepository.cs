using Microsoft.EntityFrameworkCore;
using Prodavalnik.Areas.Identity.Data;
using Prodavalnik.Core.IReposotories;
using Prodavalnik.Models;

namespace Prodavalnik.Core.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, ILogger logger) 
            : base(context, logger)
        {
        }
        public override async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await base.GetAll();
            }
            catch (Exception ex)
            {

                this._logger.LogError(ex, "{Repo} GetAll method error", typeof(ProductRepository));
                return new List<Product>();
            }
        }
        public override async Task<bool> Upsert(Product entity)
        {

            try
            {
                var existingProduct = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingProduct == null)
                {
                    return await Add(entity);
                }
                existingProduct.Name = entity.Name;
                existingProduct.Description = entity.Description;
                existingProduct.AddedOn = entity.AddedOn;
                existingProduct.Price=entity.Price;
                existingProduct.Img=entity.Img;
                existingProduct.Category=entity.Category;
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
                var existingProduct = await dbSet.Where(x => x.Id == int.Parse(id)).FirstOrDefaultAsync();
                if (existingProduct != null)
                {
                    dbSet.Remove(existingProduct);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                this._logger.LogError(ex, "{Repo} Delete method error", typeof(ProductRepository));
                return false;
            }
        }
    }
}
