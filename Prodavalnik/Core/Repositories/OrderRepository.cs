using Prodavalnik.Areas.Identity.Data;
using Prodavalnik.Core.IReposotories;
using Prodavalnik.Models;

namespace Prodavalnik.Core.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }
        
    }
}
