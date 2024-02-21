namespace bangazonBE.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int customerId { get; set; }
        public bool isComplete { get; set; }
        public int paymentTypeId { get; set; }
    }
}
