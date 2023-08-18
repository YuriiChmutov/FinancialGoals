using FinancialGoals.Core.DTOs.Transaction;

namespace FinancialGoals.Client.Services.TransactionService;

public interface ITransactionService
{
    event Action OnChange;
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    public List<TransactionToReturn> Transactions { get; set; }
    public Dictionary<int, TransactionsDataDTO> TransactionsForAccounts { get; set; }

    Task GetTransactions(int page);
    Task GetTransactionsForAccount(int accountId, int page);
    Task<TransactionToReturn> GetTransaction(int transactionId);
    Task<bool> AddTransaction(TransactionToCreate transaction);
    Task<bool> UpdateTransaction(TransactionToUpdate transaction);
}