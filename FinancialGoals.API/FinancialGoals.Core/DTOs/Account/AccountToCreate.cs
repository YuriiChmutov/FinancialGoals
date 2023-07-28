using FinancialGoals.Services;

namespace FinancialGoals.Core.DTOs.Account;

public class AccountToCreate
{
    public int? UserId { get; set; }
    public CurrencyType CurrencyType { get; set; }
}