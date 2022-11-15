using Prodavalnik.Core.IReposotories;

namespace Prodavalnik.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IProductRepository Product { get; }
        IOrderRepository Order { get; }
        Task CompliteAsync();
        Task DisposeAsync();
        void Dispose();

    }
}
