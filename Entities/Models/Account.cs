namespace Entities.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsIdentified { get; set; }
        public ICollection<RechargeOperation> RechargeOperations { get; set; }
    }
}
