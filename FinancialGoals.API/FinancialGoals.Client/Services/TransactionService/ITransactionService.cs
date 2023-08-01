using FinancialGoals.Core.DTOs.Transaction;

namespace FinancialGoals.Client.Services.TransactionService;

public interface ITransactionService
{
    event Action OnChange;
    List<TransactionToReturn> Transactions { get; set; }
    Task GetTransactions(int currentUserId);
    Task<TransactionToReturn> GetTransaction(int transactionId);
    Task<bool> AddTransaction(TransactionToCreate transaction);
    // CategoryToCreate CreateNewCategory();
    // Task<bool> UpdateCategory(CategoryToUpdate category);
    // Task DeleteCategory(int id);
}