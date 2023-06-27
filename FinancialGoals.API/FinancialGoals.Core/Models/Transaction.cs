using FinancialGoals.Services;

namespace FinancialGoals.Core.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public FinancialAccount FinancialAccount { get; set; }
        public int FinancialAccountId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
