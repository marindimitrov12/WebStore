using System.ComponentModel.DataAnnotations;

namespace Prodavalnik.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(45)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public byte[] Img { get; set; }
        public DateTime AddedOn { get; set; }
        public Categories Category { get; set; }
    }
}
