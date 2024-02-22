namespace bangazonBE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int QuantityAvailable { get; set; }
        public int SellerId { get; set; }
        public ICollection<Order>? orders { get; set; }

    }
}
