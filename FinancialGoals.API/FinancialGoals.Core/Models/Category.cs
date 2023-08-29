using FinancialGoals.Services;

namespace FinancialGoals.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Limit { get; set; }
        public string? ImageName { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public FinancialAccount FinancialAccount { get; set; }
        public int FinancialAccountId { get; set; }
        public bool Default { get; set; } = false;
        public TransactionType TransactionType { get; set; } = TransactionType.Expense;
        public string Color { get; set; } = String.Empty;
    }
}
