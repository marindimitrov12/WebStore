using Prodavalnik.Models;
using System.Data.Entity;

namespace Prodavalnik.Data
{
    public interface IApplicationDbContext:IDisposable
    {
         DbSet<Product> Products { get; set; }
         DbSet<Order> Orders { get; set; }
    }
}
