namespace Prodavalnik.Models
{
    public class CardItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total { get => Quantity * Price; }
        public byte[] Image { get; set; }
        public CardItem(Product product)
        {
            if (product!=null)
            {
                this.ProductId = product.Id;
                this.ProductName = product.Name;
                this.Image = product.Img;
                this.Price = product.Price;
                this.Quantity = 1;
            }
           

        }
    }
}
