using FinancialGoals.Services;

namespace FinancialGoals.Core.DTOs.Transaction;

public class TransactionToCreate
{
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public int FinancialAccountId { get; set; }
    public int CategoryId { get; set; }
}