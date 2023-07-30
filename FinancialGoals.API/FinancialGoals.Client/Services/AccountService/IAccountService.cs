using FinancialGoals.Core.DTOs.Account;

namespace FinancialGoals.Client.Services.AccountService;

public interface IAccountService
{
    event Action OnChange;
    int CurrentAccountId { get; set; }
    List<AccountToReturn> Accounts { get; set; }
    Task GetAccounts();
    Task Clean();
    // Task<AccountToReturn> GetAccount();
    // Task<bool> AddAccount();
}