using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Repository.AccountService;

public interface IAccountService
{
    Task<List<FinancialAccount>> GetUserAccountsAsync(int userId);
    Task<int> GetAmountOfUsersCurrencyAccounts(int userId, CurrencyType currencyType);
    Task<bool> AccountExistsAsync(int accountId);
    Task<FinancialAccount?> GetAccountAsync(int accountId);
    Task AddAccountAsync(FinancialAccount account, int userId);
    Task UpdateAccountAsync(int accountId, FinancialAccount account);
    Task DeleteAccountAsync(int accountId);
}