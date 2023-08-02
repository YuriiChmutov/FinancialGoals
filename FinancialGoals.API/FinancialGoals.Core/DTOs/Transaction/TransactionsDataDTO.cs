namespace FinancialGoals.Core.DTOs.Transaction;

public class TransactionsDataDTO
{
    public List<TransactionToReturn> Transactions { get; set; } = new List<TransactionToReturn>();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}