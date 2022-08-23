namespace CustomerManagement.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string? FirstName { get; set; } = null;
        public string LastName { get; set; }=String.Empty;
        public string? PhoneNumber { get; set; }= null;
        public string? Email { get; set; }= null;
        public decimal? TotalPurchasesAmount { get; set; }= null;
    }
}