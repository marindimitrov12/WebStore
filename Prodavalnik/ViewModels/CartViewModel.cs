using Prodavalnik.Models;

namespace Prodavalnik.ViewModels
{
    public class CartViewModel
    {
        public List<CardItem> CardItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
