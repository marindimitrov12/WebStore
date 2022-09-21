using Prodavalnik.Areas.Identity.Data;
using Prodavalnik.Core.IConfiguration;
using Prodavalnik.Core.IReposotories;
using Prodavalnik.Core.Repositories;

namespace Prodavalnik.Data
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public IUserRepository User { get; private set; }
        public IProductRepository Product { get; private set; }
        public IOrderRepository Order { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            User=new UserRepository(_context, _logger);
            Product=new ProductRepository(_context, _logger);
            Order=new OrderRepository(_context, _logger);
        }
        public async Task CompliteAsync()
        {
            await _context.SaveChangesAsync();  
        }

        public async Task DisposeAsync()
        {
          await _context.DisposeAsync();
        }
        public void  Dispose()
        {
             _context.DisposeAsync();
        }
    }
}
