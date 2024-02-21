namespace bangazonBE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public int categoryId { get; set; }
        public decimal pricePerUnit { get; set; }
        public int quantityAvailable { get; set; }
        public int sellerId { get; set; }

    }
}
