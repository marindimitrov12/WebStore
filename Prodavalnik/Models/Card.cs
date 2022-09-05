using Prodavalnik.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodavalnik.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
