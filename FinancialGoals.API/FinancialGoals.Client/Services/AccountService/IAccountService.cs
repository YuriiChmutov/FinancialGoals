using FinancialGoals.Core.DTOs.Account;

namespace FinancialGoals.Client.Services.AccountService;

public interface IAccountService
{
    event Action OnChange;
    List<AccountToReturn> Accounts { get; set; }
    Task GetAccounts();
}