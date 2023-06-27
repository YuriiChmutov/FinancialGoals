namespace FinancialGoals.Core.Models
{
    public class FinancialAccount
    {
        public int FinancialAccountId { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public List<Goal>? Goals { get; set; }
    }
}
