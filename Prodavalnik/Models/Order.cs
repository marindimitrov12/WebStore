using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodavalnik.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; }
       
        
    }
}
