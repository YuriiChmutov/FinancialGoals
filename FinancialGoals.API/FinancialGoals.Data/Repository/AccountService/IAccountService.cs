using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Repository.AccountService;

public interface IAccountService
{
    Task<List<FinancialAccount>> GetUserAccountsAsync(int userId);
    Task<bool> AccountExistsAsync(int accountId);
    Task<FinancialAccount?> GetAccountAsync(int accountId);
    Task AddAccountAsync(FinancialAccount account);
    Task UpdateAccountAsync(int accountId, FinancialAccount account);
    Task DeleteAccountAsync(int accountId);
}