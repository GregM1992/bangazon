
namespace bangazonBE.Models

{
    public class User
    {
        public int Id { get; set; }
        public string? uId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string userName {  get; set; }
        public string address { get; set; }
        public bool isSeller { get; set; }
    }
}
