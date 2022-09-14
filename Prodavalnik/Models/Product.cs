using Microsoft.AspNetCore.Identity;
using Prodavalnik.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

      
        public string Owner { get; set; }

        

    }
}
