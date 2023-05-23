namespace Entities.Models
{
    public class RechargeOperation
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Account Account{ get; set; }
    }
}
