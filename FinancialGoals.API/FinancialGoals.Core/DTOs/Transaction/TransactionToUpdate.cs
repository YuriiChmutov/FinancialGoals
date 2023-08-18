using FinancialGoals.Services;

namespace FinancialGoals.Core.DTOs.Transaction
{
    public class TransactionToUpdate
    {
        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
